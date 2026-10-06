const baseUrl = "https://localhost:7089/api/girl";

let editId = 0;

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
                        <i class="fa-solid fa-users fa-2x"></i>
                        <p>No girls found</p>
                    </div>
                </td>
            </tr>`;
        } else {

            data.forEach(g => {
                html += `
                <tr>
                    <td>
                        <div class="user-cell">
                            <img src="https://i.pravatar.cc/40?u=${g.girlId}">
                            <span>${g.name}</span>
                        </div>
                    </td>
                    <td>${g.phone}</td>
                    <td>${g.guardianName}</td>
                    <td>#${g.roomName || g.roomId}</td>
                    <td>${getStatus(g.isActive)}</td>
                    <td>
                       <button class="action-btn edit" onclick="editItem(${g.girlId})">
                            <i class="fa-solid fa-pen"></i>
                        </button>

                        <button class="action-btn delete" onclick="deleteItem(${g.girlId})">
                            <i class="fa-solid fa-trash"></i>
                        </button>
                    </td>
                </tr>`;
            });
        }

        document.getElementById("tableBody").innerHTML = html;

    } catch {
        showAlert("Error loading data", true);
    }

    showLoader(false);
}

// ================= MODAL =================
function openModal() {
    editId = 0;
    clearForm();

    document.getElementById("modalTitle").innerHTML =
        `<i class="fa-solid fa-user-plus"></i> Add Girl`;

    document.getElementById("modal").classList.remove("hidden");
}

function closeModal() {
    document.getElementById("modal").classList.add("hidden");
}

// ================= SAVE =================
async function saveData() {

    if (!validate()) return;

    const model = {
        girlId: editId,
        name: name.value.trim(),
        phone: phone.value,
        guardianName: guardianName.value.trim(),
        guardianPhone: guardianPhone.value,
        roomId: parseInt(roomId.value),
        isActive: isActive.checked,
        address: address.value.trim() || "N/A" // FIXED
    };

    try {

        const method = editId ? "PUT" : "POST";

        const res = await fetch(baseUrl, {
            method: method,
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(model)
        });

        if (!res.ok) throw new Error();

        const msg = await res.text();

        showAlert(msg || "Saved successfully", false);
        closeModal();
        loadData();

    } catch {
        showAlert("Save failed", true);
    }
}

// ================= EDIT =================
async function editItem(id) {
    try {
        editId = id;

        const res = await fetch(`${baseUrl}/${id}`);
        if (!res.ok) throw new Error();

        const g = await res.json();

        name.value = g.name;
        phone.value = g.phone;
        guardianName.value = g.guardianName;
        guardianPhone.value = g.guardianPhone;
        roomId.value = g.roomId;
        address.value = g.address;
        isActive.checked = g.isActive;

        document.getElementById("modalTitle").innerHTML =
            `<i class="fa-solid fa-user-pen"></i> Edit Girl`;

        document.getElementById("modal").classList.remove("hidden");

    } catch {
        showAlert("Failed to load data", true);
    }
}

// ================= DELETE =================
async function deleteItem(id) {

    if (!confirm("Are you sure you want to delete?")) return;

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

    if (!name.value.trim()) {
        nameError.innerText = "Name required";
        valid = false;
    }

    if (!phone.value.match(/^[0-9]{10}$/)) {
        phoneError.innerText = "Invalid phone";
        valid = false;
    }

    if (!guardianName.value.trim()) {
        guardianNameError.innerText = "Required";
        valid = false;
    }

    if (!guardianPhone.value.match(/^[0-9]{10}$/)) {
        guardianPhoneError.innerText = "Invalid";
        valid = false;
    }

    if (!roomId.value || parseInt(roomId.value) <= 0) {
        roomError.innerText = "Invalid Room";
        valid = false;
    }

    if (!address.value.trim()) {
        addressError.innerText = "Address required";
        valid = false;
    }

    return valid;
}

// ================= HELPERS =================
function clearErrors() {
    document.querySelectorAll(".error").forEach(e => e.innerText = "");
}

function clearForm() {
    document.querySelectorAll("input:not([type='checkbox'])")
        .forEach(i => i.value = "");

    isActive.checked = true;
    editId = 0;
    clearErrors();
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

function getStatus(isActive) {
    return isActive
        ? `<span class="status in"><i class="fa-solid fa-circle-check"></i> Active</span>`
        : `<span class="status out"><i class="fa-solid fa-circle-xmark"></i> Inactive</span>`;
}

// ================= SEARCH =================
window.addEventListener("DOMContentLoaded", () => {
    const search = document.getElementById("searchInput");

    if (search) {
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
});

// INIT
loadData();