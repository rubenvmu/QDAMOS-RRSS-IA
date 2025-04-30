// Cambio entre vistas móvil/desktop
document.addEventListener('DOMContentLoaded', function() {
    const mobileView = document.getElementById('mobileView');
    const desktopView = document.getElementById('desktopView');
    
    // Establecer vista por defecto (desktop)
    if (!localStorage.getItem('viewMode')) {
        localStorage.setItem('viewMode', 'desktop');
    }
    
    // Aplicar vista guardada
    if (localStorage.getItem('viewMode') === 'mobile') {
        document.body.classList.add('mobile-view');
        mobileView.classList.add('active');
        desktopView.classList.remove('active');
    }
    
    // Event listeners
    mobileView.addEventListener('click', function() {
        document.body.classList.add('mobile-view');
        localStorage.setItem('viewMode', 'mobile');
        this.classList.add('active');
        desktopView.classList.remove('active');
    });
    
    desktopView.addEventListener('click', function() {
        document.body.classList.remove('mobile-view');
        localStorage.setItem('viewMode', 'desktop');
        this.classList.add('active');
        mobileView.classList.remove('active');
    });
});