const baseUrl = "https://localhost:7089/api/hostelattendance";
const girlUrl = "https://localhost:7089/api/girl";

// INIT
loadData();
loadGirls();

// ================= LOAD =================
async function loadData() {
    showLoader(true);

    try {
        const res = await fetch(`${baseUrl}/today`);
        if (!res.ok) throw new Error();

        const data = await res.json();

        let html = "";

        if (data.length === 0) {
            html = `
            <tr>
                <td colspan="3">
                    <div class="empty">
                        <i class="fa-solid fa-clipboard"></i>
                        <p>No attendance found</p>
                    </div>
                </td>
            </tr>`;
        } else {

            data.forEach(x => {
                html += `
                <tr>
                    <td>
                        <div class="user-cell">
                            <img src="https://i.pravatar.cc/40?u=${x.girlId}">
                            <span>${x.girlName}</span>
                        </div>
                    </td>

                    <td>${formatDate(x.date)}</td>

                    <td>
                        ${getStatusBadge(x.isPresent)}
                    </td>
                </tr>`;
            });
        }

        document.getElementById("attendanceTable").innerHTML = html;

    } catch {
        showAlert("Error loading data", true);
    }

    showLoader(false);
}

// ================= LOAD GIRLS =================
async function loadGirls() {
    try {
        const res = await fetch(girlUrl);
        const data = await res.json();

        let options = `<option value="">Select Girl</option>`;

        data.forEach(g => {
            options += `<option value="${g.girlId}">${g.name}</option>`;
        });

        girlId.innerHTML = options;

    } catch {
        showAlert("Failed to load girls", true);
    }
}

// ================= MODAL =================
function openModal() {
    clearForm();
    document.getElementById("modal").classList.remove("hidden");
}

function closeModal() {
    document.getElementById("modal").classList.add("hidden");
}

// ================= SAVE =================
async function saveData() {

    if (!validate()) return;

    const model = {
        girlId: parseInt(girlId.value),
        date: date.value,
        isPresent: isPresent.value === "true"
    };

    try {
        const res = await fetch(baseUrl, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(model)
        });

        if (!res.ok) throw new Error();

        showAlert("Saved successfully", false);
        closeModal();
        loadData();

    } catch {
        showAlert("Save failed", true);
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

    if (!date.value) {
        dateError.innerText = "Required";
        valid = false;
    }

    if (!isPresent.value) {
        statusError.innerText = "Required";
        valid = false;
    }

    return valid;
}

// ================= HELPERS =================
function getStatusBadge(isPresent) {
    return isPresent
        ? `<span class="status in"><i class="fa-solid fa-circle-check"></i> Present</span>`
        : `<span class="status out"><i class="fa-solid fa-circle-xmark"></i> Absent</span>`;
}

function formatDate(date) {
    return new Date(date).toLocaleDateString();
}

function clearErrors() {
    document.querySelectorAll(".error").forEach(e => e.innerText = "");
}

function clearForm() {
    girlId.value = "";
    date.value = "";
    isPresent.value = "";
    clearErrors();
}

// ================= UI =================
function showLoader(show) {
    document.getElementById("loader").classList.toggle("hidden", !show);
}

function showAlert(msg, isError) {
    const alert = document.getElementById("alert");

    alert.classList.remove("hidden");
    alert.classList.toggle("error-alert", isError);
    alert.classList.toggle("success", !isError);

    alert.innerText = msg;

    setTimeout(() => {
        alert.classList.add("hidden");
    }, 3000);
}

// ================= SEARCH =================
document.addEventListener("DOMContentLoaded", () => {
    const search = document.getElementById("searchInput");

    search.addEventListener("input", function () {
        const value = this.value.toLowerCase();
        const rows = document.querySelectorAll("#attendanceTable tr");

        rows.forEach(row => {
            row.style.display = row.innerText.toLowerCase().includes(value)
                ? ""
                : "none";
        });
    });
});