const baseUrl = "https://localhost:7089/api/entrylog";
const girlUrl = "https://localhost:7089/api/girl";

// INIT
loadData();
loadGirls();

// ================= LOAD DATA =================
async function loadData() {
    showLoader(true);

    try {
        const res = await fetch(baseUrl);
        if (!res.ok) throw new Error();

        const data = await res.json();

        let html = "";

        if (data.length === 0) {
            html = `
            <tr>
                <td colspan="6">
                    <div class="empty">
                        <i class="fa-solid fa-clock"></i>
                        <p>No logs found</p>
                    </div>
                </td>
            </tr>`;
        } else {

            data.forEach(l => {

                const isReturned = l.inTime !== null;

                html += `
                <tr>
                    <td>
                        <div class="user-cell">
                            <img src="https://i.pravatar.cc/40?u=${l.girlId}">
                            <span>${l.girlName}</span>
                        </div>
                    </td>

                    <td>${formatDate(l.outTime)}</td>
                    <td>${l.inTime ? formatDate(l.inTime) : "-"}</td>
                    <td>${l.reason}</td>

                    <td>
                        ${getStatus(isReturned)}
                    </td>

                    <td>
                        ${!isReturned ? `
                        <button class="action-btn edit" onclick="markReturn(${l.entryLogId})">
                            <i class="fa-solid fa-right-to-bracket"></i>
                        </button>` : ""}
                        
                        <button class="action-btn delete" onclick="deleteItem(${l.entryLogId})">
                            <i class="fa-solid fa-trash"></i>
                        </button>
                    </td>
                </tr>`;
            });
        }

        document.getElementById("logTable").innerHTML = html;

    } catch {
        showAlert("Error loading logs", true);
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

        document.getElementById("girlId").innerHTML = options;

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
        outTime: outTime.value,
        reason: reason.value.trim()
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

// ================= RETURN =================
async function markReturn(id) {

    if (!confirm("Mark as returned?")) return;

    try {
        const res = await fetch(`${baseUrl}/return/${id}`, {
            method: "PUT"
        });

        if (!res.ok) throw new Error();

        showAlert("Returned successfully", false);
        loadData();

    } catch {
        showAlert("Return failed", true);
    }
}

// ================= DELETE =================
async function deleteItem(id) {

    if (!confirm("Delete this log?")) return;

    try {
        const res = await fetch(`${baseUrl}/${id}`, {
            method: "DELETE"
        });

        if (!res.ok) throw new Error();

        showAlert("Deleted successfully", false);
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
function clearErrors() {
    document.querySelectorAll(".error").forEach(e => e.innerText = "");
}

function clearForm() {
    girlId.value = "";
    outTime.value = "";
    reason.value = "";
    clearErrors();
}

function formatDate(date) {
    return new Date(date).toLocaleString();
}

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

function getStatus(isReturned) {
    return isReturned
        ? `<span class="status in"><i class="fa-solid fa-circle-check"></i> Returned</span>`
        : `<span class="status out"><i class="fa-solid fa-arrow-right"></i> Outside</span>`;
}

// ================= SEARCH =================
document.addEventListener("DOMContentLoaded", () => {
    const search = document.getElementById("searchInput");

    search.addEventListener("input", function () {
        const value = this.value.toLowerCase();
        const rows = document.querySelectorAll("#logTable tr");

        rows.forEach(row => {
            row.style.display = row.innerText.toLowerCase().includes(value)
                ? ""
                : "none";
        });
    });
});