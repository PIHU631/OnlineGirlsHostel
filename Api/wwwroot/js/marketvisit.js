const baseUrl = CONFIG.BASE_URL + "/marketvisit";
const girlUrl = CONFIG.BASE_URL + "/girl";

let editId = 0;

// INIT
document.addEventListener("DOMContentLoaded", () => {
    loadData();
    loadGirls();
    initSearch();
});

// ================= LOAD =================
async function loadData() {
    showLoader(true);

    try {
        const res = await fetch(`${baseUrl}/today`);
        const data = await res.json();

        let html = "";

        if (data.length === 0) {
            html = `
            <tr>
                <td colspan="6">
                    <div class="empty">
                        <i class="fa-solid fa-store fa-2x"></i>
                        <p>No visits found</p>
                    </div>
                </td>
            </tr>`;
        } else {

            data.forEach(v => {
                const isReturned = v.inTime !== null;

                html += `
                <tr>
                    <td>
                        <div class="user-cell">
                            <img src="https://i.pravatar.cc/40?u=${v.marketVisitId}">
                            <span>${v.girlName || ""}</span>
                        </div>
                    </td>
                    <td>${formatDate(v.outTime)}</td>
                    <td>${v.inTime ? formatDate(v.inTime) : "-"}</td>
                    <td>${v.reason}</td>
                    <td>${getStatus(isReturned)}</td>
                    <td>
                        ${!isReturned ? `
                        <button class="action-btn edit" onclick="markReturn(${v.marketVisitId})">
                            <i class="fa-solid fa-right-to-bracket"></i>
                        </button>` : ""}

                        <button class="action-btn delete" onclick="deleteVisit(${v.marketVisitId})">
                            <i class="fa-solid fa-trash"></i>
                        </button>
                    </td>
                </tr>`;
            });
        }

        document.getElementById("visitTable").innerHTML = html;

    } catch {
        showAlert("Error loading data", true);
    }

    showLoader(false);
}

// ================= GIRLS =================
async function loadGirls() {
    try {
        const res = await fetch(girlUrl);
        const data = await res.json();

        let options = `<option value="">Select Girl</option>`;

        data.forEach(g => {
            options += `<option value="${g.girlId}">${g.name}</option>`;
        });

        document.getElementById("girlId").innerHTML = options;

    } catch {
        showAlert("Failed to load girls", true);
    }
}

// ================= MODAL =================
function openModal() {
    clearForm();
    document.getElementById("visitModal").classList.remove("hidden");
}

function closeModal() {
    document.getElementById("visitModal").classList.add("hidden");
}

// ================= SAVE =================
async function saveData() {

    if (!validate()) return;

    const model = {
        girlId: parseInt(girlId.value),
        outTime: outTime.value,
        reason: reason.value.trim()
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
        showAlert("Save failed", true);
    }
}

// ================= RETURN =================
async function markReturn(id) {

    if (!confirm("Mark as returned?")) return;

    try {
        const res = await fetch(`${baseUrl}/return/${id}`, {
            method: "PUT"
        });

        const msg = await res.text();

        showAlert(msg, false);
        loadData();

    } catch {
        showAlert("Return failed", true);
    }
}

// ================= DELETE =================
async function deleteVisit(id) {

    if (!confirm("Delete this visit?")) return;

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

    if (!outTime.value) {
        outError.innerText = "Required";
        valid = false;
    }

    if (!reason.value.trim()) {
        reasonError.innerText = "Required";
        valid = false;
    }

    return valid;
}

// ================= HELPERS =================
function formatDate(date) {
    return new Date(date).toLocaleString();
}

function getStatus(isReturned) {
    return isReturned
        ? `<span class="status in"><i class="fa-solid fa-circle-check"></i> Returned</span>`
        : `<span class="status out"><i class="fa-solid fa-arrow-right"></i> In Market</span>`;
}

function clearErrors() {
    document.querySelectorAll(".error").forEach(e => e.innerText = "");
}

function clearForm() {
    girlId.value = "";
    outTime.value = "";
    reason.value = "";
    clearErrors();
}

// ================= UI =================
function showLoader(show) {
    loader.classList.toggle("hidden", !show);
}

function showAlert(msg, isError) {
    const alert = document.getElementById("alertBox");

    alert.classList.remove("hidden");
    alert.classList.toggle("error-alert", isError);
    alert.classList.toggle("success", !isError);

    alert.innerText = msg;

    setTimeout(() => alert.classList.add("hidden"), 3000);
}

// ================= SEARCH =================
function initSearch() {
    const search = document.getElementById("searchInput");

    if (!search) return;

    search.addEventListener("input", function () {
        const value = this.value.toLowerCase();
        const rows = document.querySelectorAll("#visitTable tr");

        rows.forEach(row => {
            row.style.display = row.innerText.toLowerCase().includes(value)
                ? ""
                : "none";
        });
    });
}