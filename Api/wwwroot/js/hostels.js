const baseUrl = "https://localhost:7089/api/hostel";

let editId = 0;

// INIT
loadData();

// ================= LOAD =================
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
                <td colspan="7">
                    <div class="empty">
                        <i class="fa-solid fa-building"></i>
                        <p>No hostels found</p>
                    </div>
                </td>
            </tr>`;
        } else {

            data.forEach(h => {
                html += `
                <tr>
                    <td>${h.hostelName}</td>
                    <td>${h.city}</td>
                    <td>${h.address}</td>
                    <td>${h.wardenName}</td>
                    <td>${h.wardenPhone}</td>

                    <td>${getStatus(h.isActive)}</td>

                    <td>
                        <button class="action-btn edit" onclick="editItem(${h.hostelId})">
                            <i class="fa-solid fa-pen"></i>
                        </button>

                        <button class="action-btn delete" onclick="deleteItem(${h.hostelId})">
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

// ================= MODAL =================
function openModal() {
    editId = 0;
    clearForm();

    modalTitle.innerHTML = `<i class="fa-solid fa-plus"></i> Add Hostel`;

    modal.classList.remove("hidden");
}

function closeModal() {
    modal.classList.add("hidden");
}

// ================= SAVE =================
async function saveData() {

    if (!validate()) return;

    const model = {
        hostelId: editId,
        hostelName: hostelName.value.trim(),
        address: address.value.trim(),
        city: city.value.trim(),
        wardenName: wardenName.value.trim(),
        wardenPhone: wardenPhone.value,
        isActive: isActive.checked
    };

    try {

        const method = editId ? "PUT" : "POST";

        const res = await fetch(baseUrl, {
            method: method,
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

// ================= EDIT =================
async function editItem(id) {
    try {
        editId = id;

        const res = await fetch(`${baseUrl}/${id}`);
        const h = await res.json();

        hostelName.value = h.hostelName;
        address.value = h.address;
        city.value = h.city;
        wardenName.value = h.wardenName;
        wardenPhone.value = h.wardenPhone;
        isActive.checked = h.isActive;

        modalTitle.innerHTML = `<i class="fa-solid fa-pen"></i> Edit Hostel`;

        modal.classList.remove("hidden");

    } catch {
        showAlert("Failed to load data", true);
    }
}

// ================= DELETE =================
async function deleteItem(id) {

    if (!confirm("Delete this hostel?")) return;

    try {
        const res = await fetch(`${baseUrl}/${id}`, { method: "DELETE" });

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

    if (!hostelName.value.trim()) {
        nameError.innerText = "Required";
        valid = false;
    }

    if (!city.value.trim()) {
        cityError.innerText = "Required";
        valid = false;
    }

    if (!address.value.trim()) {
        addressError.innerText = "Required";
        valid = false;
    }

    if (!wardenName.value.trim()) {
        wardenError.innerText = "Required";
        valid = false;
    }

    if (!wardenPhone.value.match(/^[0-9]{10}$/)) {
        wardenPhoneError.innerText = "Invalid phone";
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
    clearErrors();
}

function getStatus(isActive) {
    return isActive
        ? `<span class="status in"><i class="fa-solid fa-circle-check"></i> Active</span>`
        : `<span class="status out"><i class="fa-solid fa-circle-xmark"></i> Inactive</span>`;
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

    setTimeout(() => {
        alert.classList.add("hidden");
    }, 3000);
}

// ================= SEARCH =================
document.addEventListener("DOMContentLoaded", () => {
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
});