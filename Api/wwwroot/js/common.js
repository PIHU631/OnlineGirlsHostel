async function loadSidebar() {
    const res = await fetch("/shared/sidebar.html");
    const html = await res.text();
    document.getElementById("sidebar").innerHTML = html;
}

loadSidebar();