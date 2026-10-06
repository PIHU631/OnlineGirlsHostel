const baseUrl = "https://localhost:7089/api/visitor";

let editId = 0;

// INIT
loadVisitors();

// ================= LOAD =================
async function loadVisitors() {
    showLoader(true);

    try {
        const res = await fetch(baseUrl);
        const data = await res.json();

        let html = "";

        data.forEach(v => {
            html += `
            <tr>
                <td>${v.visitorId}</td>
                <td>${v.name}</td>
                <td>${v.phone}</td>
                <td>${v.relation}</td>
                <td>${v.idProofNumber}</td>
                <td>
                    <button class="btn danger small" onclick="deleteVisitor(${v.visitorId})">
                        <i class="fa-solid fa-trash"></i>
                    </button>
                </td>
            </tr>`;
        });

        document.getElementById("visitorTable").innerHTML = html;

    } catch {
        showAlert("Error loading visitors", true);
    }

    showLoader(false);
}

// ================= MODAL =================
function openModal() {
    editId = 0;
    clearForm();
    document.getElementById("visitorModal").classList.remove("hidden");
}

function closeModal() {
    document.getElementById("visitorModal").classList.add("hidden");
}

// ================= SAVE =================
async function saveData() {

    if (!validate()) return;

    const model = {
        name: document.getElementById("name").value,
        phone: document.getElementById("phone").value,
        relation: document.getElementById("relation").value,
        idProofNumber: document.getElementById("idProof").value
    };

    try {
        const res = await fetch(baseUrl, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(model)
        });

        const msg = await res.text();

        showAlert(msg || "Saved successfully", false);
        closeModal();
        loadVisitors();

    } catch {
        showAlert("Save failed", true);
    }
}

// ================= DELETE =================
async function deleteVisitor(id) {

    if (!confirm("Delete this visitor?")) return;

    try {
        const res = await fetch(`${baseUrl}/${id}`, {
            method: "DELETE"
        });

        const msg = await res.text();

        showAlert(msg || "Deleted successfully", false);
        loadVisitors();

    } catch {
        showAlert("Delete failed", true);
    }
}

// ================= VALIDATION =================
function validate() {

    let valid = true;
    clearErrors();

    const name = document.getElementById("name").value.trim();
    const phone = document.getElementById("phone").value;
    const relation = document.getElementById("relation").value.trim();
    const idProof = document.getElementById("idProof").value.trim();

    if (!name) {
        document.getElementById("nameError").innerText = "Required";
        valid = false;
    }

    if (!/^[0-9]{10}$/.test(phone)) {
        document.getElementById("phoneError").innerText = "Invalid phone";
        valid = false;
    }

    if (!relation) {
        document.getElementById("relationError").innerText = "Required";
        valid = false;
    }

    if (!idProof) {
        document.getElementById("idProofError").innerText = "Required";
        valid = false;
    }

    return valid;
}

// ================= HELPERS =================
function clearErrors() {
    document.querySelectorAll(".error").forEach(e => e.innerText = "");
}

function clearForm() {
    document.getElementById("name").value = "";
    document.getElementById("phone").value = "";
    document.getElementById("relation").value = "";
    document.getElementById("idProof").value = "";
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