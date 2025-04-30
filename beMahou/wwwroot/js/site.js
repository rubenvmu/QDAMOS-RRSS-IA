// Función para cambiar entre vistas móvil/desktop
function toggleViewMode(isMobile) {
    const mobileCss = document.getElementById('mobile-css');
    const createButton = document.getElementById('createPostButton');
    const navbarTop = document.querySelector('.navbar'); // Navbar superior
    const footer = document.getElementById('mahou-footer');

    if (isMobile) {
        document.body.classList.add('mobile-view');
        mobileCss.removeAttribute('disabled');
        localStorage.setItem('viewMode', 'mobile');

        // Ocultar navbar superior y botón flotante
        if (navbarTop) navbarTop.style.display = 'none';
        if (createButton) createButton.style.display = 'none';

        // Crear navbar inferior dinámico si no existe
        if (!footer) createMobileFooter();
    } else {
        document.body.classList.remove('mobile-view');
        mobileCss.setAttribute('disabled', 'true');
        localStorage.setItem('viewMode', 'desktop');

        // Mostrar navbar superior y botón flotante
        if (navbarTop) navbarTop.style.display = 'flex';
        if (createButton) createButton.style.display = 'flex';

        // Eliminar navbar inferior dinámico si existe
        const existingFooter = document.getElementById('mahou-footer');
        if (existingFooter) existingFooter.remove();
    }
}

// Función para crear el footer dinámico en modo móvil
function createMobileFooter() {
    const footerHtml = `
        <footer id="mahou-footer" class="mahou-footer">
            <nav id="footer-nav" class="footer-nav-container">
                <!-- Ícono Inicio -->
                <div id="footer-home" class="footer-nav-item">
                    <a href="/Index" class="footer-nav-link">
                        <i class="fas fa-home footer-icon"></i>
                        <span class="footer-label">Inicio</span>
                    </a>
                </div>

                <!-- Ícono Festivales -->
                <div id="footer-festivals" class="footer-nav-item">
                    <a href="/Categoria/Festival" class="footer-nav-link">
                        <i class="fas fa-music footer-icon"></i>
                        <span class="footer-label">Conciertos</span>
                    </a>
                </div>

                <!-- Botón Central (Crear Publicación) -->
                <div id="footer-center-btn" class="footer-center-button">
                    <a href="/NuevaPublicacion" class="footer-main-button">
                        <i class="fas fa-plus"></i>
                    </a>
                </div>

                <!-- Ícono Quedadas -->
                <div id="footer-meetups" class="footer-nav-item">
                    <a href="/Categoria/Concurso" class="footer-nav-link">
                        <i class="fas fa-users footer-icon"></i>
                        <span class="footer-label">Quedadas</span>
                    </a>
                </div>

                <!-- Ícono Perfil -->
                <div id="footer-profile" class="footer-nav-item">
                    <a href="/Perfil" class="footer-nav-link">
                        <i class="fas fa-user footer-icon"></i>
                        <span class="footer-label">Sorteos</span>
                    </a>
                </div>
            </nav>
        </footer>
        <div class="position-fixed bottom-0 end-0 m-3">
        <div class="btn-group">
            <button id="mobileView" class="btn btn-outline-dark" title="Vista móvil">
                <i class="fas fa-mobile-alt"></i>
            </button>
            <button id="desktopView" class="btn btn-dark" title="Vista de escritorio">
                <i class="fas fa-desktop"></i>
            </button>
        </div>
    </div>
    `;

    document.body.insertAdjacentHTML('beforeend', footerHtml);
    setupFooterInteractions();
}

// Configurar interacciones del footer
function setupFooterInteractions() {
    const footerItems = document.querySelectorAll('#mahou-footer .footer-nav-item');
    const footerButton = document.querySelector('#mahou-footer .footer-main-button');

    // Efecto táctil para móviles
    footerItems.forEach(item => {
        item.addEventListener('touchstart', function() {
            this.style.transform = 'translateY(-8px)';
            this.querySelector('.footer-icon').style.transform = 'scale(1.1)';
        });
        
        item.addEventListener('touchend', function() {
            this.style.transform = '';
            this.querySelector('.footer-icon').style.transform = '';
        });
    });
    
    if (footerButton) {
        footerButton.addEventListener('touchstart', function() {
            this.style.transform = 'translate(-50%, -22px) scale(1.05)';
        });
        
        footerButton.addEventListener('touchend', function() {
            this.style.transform = 'translate(-50%, -20px)';
        });
    }
}

// Inicialización al cargar la página
document.addEventListener('DOMContentLoaded', function () {
    const mobileViewBtn = document.getElementById('mobileView');
    const desktopViewBtn = document.getElementById('desktopView');

    // Establecer vista según preferencia guardada
    const savedViewMode = localStorage.getItem('viewMode') || 'desktop';
    toggleViewMode(savedViewMode === 'mobile');

    // Event listeners para los botones
    if (mobileViewBtn && desktopViewBtn) {
        mobileViewBtn.addEventListener('click', function () {
            toggleViewMode(true);
            this.classList.add('active');
            desktopViewBtn.classList.remove('active');
        });

        desktopViewBtn.addEventListener('click', function () {
            toggleViewMode(false);
            this.classList.add('active');
            mobileViewBtn.classList.remove('active');
        });
    }
});