const baseUrl = CONFIG.BASE_URL + "/roomhistory";
const girlUrl = CONFIG.BASE_URL + "/girl";
const roomUrl = CONFIG.BASE_URL + "/room";

// INIT
document.addEventListener("DOMContentLoaded", () => {
    loadData();
    loadGirls();
    loadRooms();
    initSearch();
});

// ================= LOAD =================
async function loadData() {
    showLoader(true);

    try {
        const res = await fetch(baseUrl);
        const data = await res.json();

        let html = "";

        if (data.length === 0) {
            html = `
            <tr>
                <td colspan="6">
                    <div class="empty">
                        <p>No history found</p>
                    </div>
                </td>
            </tr>`;
        } else {

            data.forEach(r => {

                const isActive = !r.endDate;

                html += `
                <tr>
                    <td>${r.girlName || r.girlId}</td>
                    <td>#${r.roomNumber || r.roomId}</td>
                    <td>${formatDate(r.startDate)}</td>
                    <td>${r.endDate ? formatDate(r.endDate) : "-"}</td>
                    <td>${getStatus(isActive)}</td>
                    <td>
                        ${isActive ? `
                        <button class="action-btn edit" onclick="endRoom(${r.girlId})">
                            <i class="fa-solid fa-right-from-bracket"></i>
                        </button>` : ""}

                        <button class="action-btn delete" onclick="deleteItem(${r.roomHistoryId})">
                            <i class="fa-solid fa-trash"></i>
                        </button>
                    </td>
                </tr>`;
            });
        }

        tableBody.innerHTML = html;

    } catch {
        showAlert("Error loading data", true);
    }

    showLoader(false);
}

// ================= LOAD DROPDOWNS =================
async function loadGirls() {
    const res = await fetch(girlUrl);
    const data = await res.json();

    let options = `<option value="">Select Girl</option>`;
    data.forEach(g => {
        options += `<option value="${g.girlId}">${g.name}</option>`;
    });

    girlId.innerHTML = options;
}

async function loadRooms() {
    const res = await fetch(roomUrl);
    const data = await res.json();

    let options = `<option value="">Select Room</option>`;
    data.forEach(r => {
        options += `<option value="${r.roomId}">${r.roomNumber}</option>`;
    });

    roomId.innerHTML = options;
}

// ================= MODAL =================
function openModal() {
    clearForm();
    modal.classList.remove("hidden");
}

function closeModal() {
    modal.classList.add("hidden");
}

// ================= SAVE =================
async function saveData() {

    if (!validate()) return;

    const model = {
        girlId: parseInt(girlId.value),
        roomId: parseInt(roomId.value),
        startDate: startDate.value
    };

    try {
        const res = await fetch(baseUrl, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(model)
        });

        const msg = await res.text();

        showAlert(msg, false);
        closeModal();
        loadData();

    } catch {
        showAlert("Assign failed", true);
    }
}

// ================= END =================
async function endRoom(girlIdVal) {

    if (!confirm("End current room?")) return;

    try {
        const res = await fetch(`${baseUrl}/end/${girlIdVal}`, {
            method: "PUT"
        });

        const msg = await res.text();

        showAlert(msg, false);
        loadData();

    } catch {
        showAlert("Failed", true);
    }
}

// ================= DELETE =================
async function deleteItem(id) {

    if (!confirm("Delete history?")) return;

    try {
        const res = await fetch(`${baseUrl}/${id}`, {
            method: "DELETE"
        });

        const msg = await res.text();

        showAlert(msg, false);
        loadData();

    } catch {
        showAlert("Delete failed", true);
    }
}

// ================= VALIDATION =================
function validate() {

    let valid = true;
    clearErrors();

    if (!girlId.value) {
        girlError.innerText = "Required";
        valid = false;
    }

    if (!roomId.value) {
        roomError.innerText = "Required";
        valid = false;
    }

    if (!startDate.value) {
        dateError.innerText = "Required";
        valid = false;
    }

    return valid;
}

// ================= HELPERS =================
function getStatus(active) {
    return active
        ? `<span class="status in">Active</span>`
        : `<span class="status out">Ended</span>`;
}

function formatDate(date) {
    return new Date(date).toLocaleDateString();
}

function clearErrors() {
    document.querySelectorAll(".error").forEach(e => e.innerText = "");
}

function clearForm() {
    girlId.value = "";
    roomId.value = "";
    startDate.value = "";
    clearErrors();
}

// ================= UI =================
function showLoader(show) {
    loader.classList.toggle("hidden", !show);
}

function showAlert(msg, isError) {
    const alert = document.getElementById("alert");

    alert.classList.remove("hidden");
    alert.classList.toggle("error-alert", isError);
    alert.classList.toggle("success", !isError);

    alert.innerText = msg;

    setTimeout(() => alert.classList.add("hidden"), 3000);
}

// ================= SEARCH =================
function initSearch() {
    const search = document.getElementById("searchInput");

    search.addEventListener("input", function () {
        const value = this.value.toLowerCase();
        const rows = document.querySelectorAll("#tableBody tr");

        rows.forEach(row => {
            row.style.display = row.innerText.toLowerCase().includes(value)
                ? ""
                : "none";
        });
    });
}