const baseUrl = "https://localhost:7089/api/room";
const hostelId = 1;

let editId = 0;

// INIT
loadData();

// ================= LOAD =================
async function loadData() {
    showLoader(true);

    try {
        const res = await fetch(baseUrl);
        const data = await res.json();

        let html = "";

        data.forEach(r => {
            html += `
            <tr>
                <td>${r.roomNumber}</td>
                <td>${r.floor}</td>
                <td>${r.capacity}</td>
                <td>${r.occupancy}</td>
                <td>
                    <span class="status ${r.isActive ? 'in' : 'out'}">
                        ${r.isActive ? "Active" : "Inactive"}
                    </span>
                </td>
                <td>
                    <button class="btn small" onclick="editItem(${r.roomId})">
                        <i class="fa-solid fa-pen"></i>
                    </button>

                    <button class="btn danger small" onclick="deleteItem(${r.roomId})">
                        <i class="fa-solid fa-trash"></i>
                    </button>
                </td>
            </tr>`;
        });

        document.getElementById("tableBody").innerHTML = html;

    } catch {
        showAlert("Error loading rooms", true);
    }

    showLoader(false);
}

// ================= MODAL =================
function openModal() {
    editId = 0;
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
        roomId: editId,
        hostelId: hostelId,
        roomNumber: document.getElementById("roomNumber").value,
        floor: parseInt(document.getElementById("floor").value),
        capacity: parseInt(document.getElementById("capacity").value),
        isActive: document.getElementById("isActive").checked
    };

    try {
        const method = editId ? "PUT" : "POST";

        const res = await fetch(baseUrl, {
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
        const r = await res.json();

        document.getElementById("roomNumber").value = r.roomNumber;
        document.getElementById("floor").value = r.floor;
        document.getElementById("capacity").value = r.capacity;
        document.getElementById("isActive").checked = r.isActive;

        document.getElementById("modal").classList.remove("hidden");

    } catch {
        showAlert("Failed to load room", true);
    }
}

// ================= DELETE =================
async function deleteItem(id) {

    if (!confirm("Delete this room?")) return;

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

    const roomNumber = document.getElementById("roomNumber").value.trim();
    const floor = document.getElementById("floor").value;
    const capacity = document.getElementById("capacity").value;

    if (!roomNumber) {
        document.getElementById("roomNumberError").innerText = "Required";
        valid = false;
    }

    if (!floor || floor <= 0) {
        document.getElementById("floorError").innerText = "Invalid";
        valid = false;
    }

    if (!capacity || capacity <= 0) {
        document.getElementById("capacityError").innerText = "Invalid";
        valid = false;
    }

    return valid;
}

// ================= HELPERS =================
function clearErrors() {
    document.querySelectorAll(".error").forEach(e => e.innerText = "");
}

function clearForm() {
    document.querySelectorAll("input").forEach(i => {
        if (i.type === "checkbox") i.checked = true;
        else i.value = "";
    });
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