const baseUrl = "https://localhost:7089/api/staff";
const hostelUrl = "https://localhost:7089/api/hostel";

let editId = 0;

// INIT
loadData();
loadHostels();

// ================= LOAD STAFF =================
async function loadData() {
    showLoader(true);

    try {
        const res = await fetch(baseUrl);
        const data = await res.json();

        let html = "";

        data.forEach(s => {
            html += `
            <tr>
                <td>${s.staffId}</td>
                <td>${s.hostelName || ""}</td>
                <td>${s.name}</td>
                <td>${getRoleName(s.role)}</td>
                <td>${s.phone}</td>
                <td>
                    <button class="btn small" onclick="editItem(${s.staffId})">
                        <i class="fa-solid fa-pen"></i>
                    </button>

                    <button class="btn danger small" onclick="deleteItem(${s.staffId})">
                        <i class="fa-solid fa-trash"></i>
                    </button>
                </td>
            </tr>`;
        });

        document.getElementById("staffTable").innerHTML = html;

    } catch {
        showAlert("Error loading staff", true);
    }

    showLoader(false);
}

// ================= LOAD HOSTELS =================
async function loadHostels() {
    try {
        const res = await fetch(hostelUrl);
        const data = await res.json();

        let options = `<option value="">Select Hostel</option>`;

        data.forEach(h => {
            options += `<option value="${h.hostelId}">${h.hostelName}</option>`;
        });

        document.getElementById("hostelId").innerHTML = options;

    } catch {
        showAlert("Failed to load hostels", true);
    }
}

// ================= ROLE =================
function getRoleName(role) {
    switch (role) {
        case 1: return "Warden";
        case 2: return "Guard";
        case 3: return "Cleaner";
        default: return "";
    }
}

// ================= MODAL =================
function openModal() {
    editId = 0;
    clearForm();
    document.getElementById("staffModal").classList.remove("hidden");
}

function closeModal() {
    document.getElementById("staffModal").classList.add("hidden");
}

// ================= SAVE =================
async function saveData() {

    if (!validate()) return;

    const model = {
        staffId: editId,
        hostelId: parseInt(document.getElementById("hostelId").value),
        name: document.getElementById("name").value,
        role: parseInt(document.getElementById("role").value),
        phone: document.getElementById("phone").value
    };

    try {
        const method = editId ? "PUT" : "POST";
        const url = editId ? `${baseUrl}/${editId}` : baseUrl;

        const res = await fetch(url, {
            method: method,
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

// ================= EDIT =================
async function editItem(id) {

    editId = id;

    try {
        const res = await fetch(`${baseUrl}/${id}`);
        const s = await res.json();

        document.getElementById("hostelId").value = s.hostelId;
        document.getElementById("name").value = s.name;
        document.getElementById("role").value = s.role;
        document.getElementById("phone").value = s.phone;

        document.getElementById("staffModal").classList.remove("hidden");

    } catch {
        showAlert("Error loading staff", true);
    }
}

// ================= DELETE =================
async function deleteItem(id) {

    if (!confirm("Delete this staff?")) return;

    try {
        const res = await fetch(`${baseUrl}/${id}`, { method: "DELETE" });

        const msg = await res.text();

        showAlert(msg || "Deleted successfully", false);
        loadData();

    } catch {
        showAlert("Delete failed", true);
    }
}

// ================= VALIDATION =================
function validate() {

    let valid = true;
    clearErrors();

    const hostelId = document.getElementById("hostelId").value;
    const name = document.getElementById("name").value.trim();
    const role = document.getElementById("role").value;
    const phone = document.getElementById("phone").value;

    if (!hostelId) {
        document.getElementById("hostelError").innerText = "Required";
        valid = false;
    }

    if (!name) {
        document.getElementById("nameError").innerText = "Required";
        valid = false;
    }

    if (!role) {
        document.getElementById("roleError").innerText = "Required";
        valid = false;
    }

    if (!/^[0-9]{10}$/.test(phone)) {
        document.getElementById("phoneError").innerText = "Invalid phone";
        valid = false;
    }

    return valid;
}

// ================= HELPERS =================
function clearErrors() {
    document.querySelectorAll(".error").forEach(e => e.innerText = "");
}

function clearForm() {
    document.getElementById("hostelId").value = "";
    document.getElementById("name").value = "";
    document.getElementById("role").value = "";
    document.getElementById("phone").value = "";
    clearErrors();
}

function showLoader(show) {
    document.getElementById("loader").classList.toggle("hidden", !show);
}

function showAlert(msg, isError) {
    const alertBox = document.getElementById("alertBox");

    alertBox.innerText = msg;
    alertBox.classList.remove("hidden");
    alertBox.style.color = isError ? "red" : "green";

    setTimeout(() => alertBox.classList.add("hidden"), 3000);
}