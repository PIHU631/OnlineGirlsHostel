const BASE_URL = CONFIG.BASE_URL;

// ================= COMMON API =================
async function getApi(url) {
    try {
        const res = await fetch(BASE_URL + url);

        if (!res.ok) {
            throw new Error(`API Error: ${res.status}`);
        }

        return await res.json();

    } catch (err) {
        console.error("API Failed:", url, err);
        throw err;
    }
}

// ================= UI HELPERS =================
function setText(id, value) {
    const el = document.getElementById(id);
    if (el) el.innerText = value ?? 0;
}

function showLoader(show) {
    const loader = document.getElementById("loader");
    if (loader) loader.classList.toggle("hidden", !show);
}

function showError(message) {
    const el = document.getElementById("errorBox");

    if (!el) return;

    el.innerText = message;
    el.classList.remove("hidden");
}

// ================= LOAD DASHBOARD =================
async function loadDashboard() {

    showLoader(true);

    try {
        // 🚀 Parallel API calls
        const [allGirls, activeGirls] = await Promise.all([
            getApi("/girl"),
            getApi("/girl/active")
        ]);

        // ===== Girls Stats =====
        const totalGirls = allGirls.length;
        const activeCount = activeGirls.length;
        const inactiveCount = totalGirls - activeCount;

        // ===== Rooms Used (unique roomIds only) =====
        const roomsUsed = [...new Set(
            allGirls
                .filter(x => x.roomId) // ignore null/0
                .map(x => x.roomId)
        )].length;

        // ===== Bind UI =====
        setText("totalGirls", totalGirls);
        setText("activeGirls", activeCount);
        setText("inactiveGirls", inactiveCount);
        setText("roomsUsed", roomsUsed);

    } catch (err) {
        showError("⚠️ Failed to load dashboard data");
    }

    showLoader(false);
}

// ================= INIT =================
document.addEventListener("DOMContentLoaded", loadDashboard);