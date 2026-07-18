class WebApp extends AppBase {
    bindUI() {
    }
}

window.App = window.App || new WebApp(window.__APP_CONFIG__ ?? {});
document.addEventListener("DOMContentLoaded", () => App.init());