// Función principal cuando el DOM está listo
document.addEventListener('DOMContentLoaded', function() {
    // Configurar botones de like
    setupLikeButtons();
    
    // Configurar botones de comentarios
    setupCommentButtons();
    
    // Configurar botones de compartir
    setupShareButtons();
    
    // Configurar eventos táctiles para móviles
    setupTouchEvents();
    
    // Configurar carga perezosa de imágenes
    setupLazyLoading();
});

// Configuración de botones de like
function setupLikeButtons() {
    const likeButtons = document.querySelectorAll('.be-mahou-like-btn');
    
    likeButtons.forEach(button => {
        // Evento para dar like
        button.addEventListener('click', async function() {
            const postId = this.getAttribute('data-id');
            const icon = this.querySelector('i');
            const countElement = this.querySelector('.be-mahou-like-count');
            
            try {
                const response = await fetch(`/NuevaPublicacion?handler=IncrementarEstrella&id=${postId}`, {
                    method: 'POST',
                    headers: {
                        'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
                    }
                });
                
                if (response.ok) {
                    const data = await response.json();
                    
                    // Actualizar contador
                    if (countElement) {
                        countElement.textContent = data.estrellas;
                    }
                    
                    // Cambiar icono y estilo
                    icon.classList.replace('far', 'fas');
                    this.classList.add('active');
                    
                    // Animación
                    animateLikeButton(this);
                    
                    // Efecto de partículas
                    createLikeEffect(this);
                }
            } catch (error) {
                console.error('Error al dar like:', error);
            }
        });
        
        // Efecto hover
        button.addEventListener('mouseenter', function() {
            if (!this.classList.contains('active')) {
                this.querySelector('i').style.transform = 'scale(1.1)';
            }
        });
        
        button.addEventListener('mouseleave', function() {
            if (!this.classList.contains('active')) {
                this.querySelector('i').style.transform = '';
            }
        });
    });
}

// Animación del botón de like
function animateLikeButton(button) {
    button.style.transform = 'scale(1.1)';
    setTimeout(() => {
        button.style.transform = '';
    }, 200);
}

// Efecto visual para likes
function createLikeButton(button) {
    const effect = document.createElement('div');
    effect.className = 'be-mahou-like-effect';
    effect.innerHTML = '<i class="fas fa-star"></i>';
    
    // Posicionamiento
    const rect = button.getBoundingClientRect();
    effect.style.left = `${rect.left + rect.width/2 - 10}px`;
    effect.style.top = `${rect.top - 20}px`;
    
    // Estilos
    effect.style.position = 'fixed';
    effect.style.color = '#ffc107';
    effect.style.fontSize = '20px';
    effect.style.pointerEvents = 'none';
    effect.style.zIndex = '1000';
    effect.style.animation = 'be-mahou-float-up 1s ease-out forwards';
    
    document.body.appendChild(effect);
    
    // Eliminar después de la animación
    setTimeout(() => {
        effect.remove();
    }, 1000);
}

// Configuración de botones de comentarios
function setupCommentButtons() {
    const commentButtons = document.querySelectorAll('.be-mahou-comment-btn');
    
    commentButtons.forEach(button => {
        button.addEventListener('click', function() {
            // Aquí iría la lógica para mostrar comentarios
            console.log('Mostrar comentarios');
        });
    });
}

// Configuración de botones de compartir
function setupShareButtons() {
    const shareButtons = document.querySelectorAll('.be-mahou-share-btn');
    
    shareButtons.forEach(button => {
        button.addEventListener('click', function() {
            // Aquí iría la lógica para compartir
            if (navigator.share) {
                navigator.share({
                    title: 'Publicación de beMahou',
                    text: 'Mira esta publicación en beMahou',
                    url: window.location.href
                }).catch(err => {
                    console.log('Error al compartir:', err);
                });
            } else {
                // Fallback para navegadores que no soportan Web Share API
                console.log('Web Share API no soportada');
            }
        });
    });
}

// Configuración de eventos táctiles para móviles
function setupTouchEvents() {
    const actionButtons = document.querySelectorAll('.be-mahou-action-btn');
    
    actionButtons.forEach(button => {
        button.addEventListener('touchstart', function() {
            this.style.transform = 'scale(0.95)';
        });
        
        button.addEventListener('touchend', function() {
            this.style.transform = '';
        });
    });
}

// Configuración de carga perezosa de imágenes
function setupLazyLoading() {
    const lazyImages = document.querySelectorAll('.be-mahou-post-image[loading="lazy"]');
    
    if ('IntersectionObserver' in window) {
        const imageObserver = new IntersectionObserver((entries, observer) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    const image = entry.target;
                    image.src = image.dataset.src || image.src;
                    imageObserver.unobserve(image);
                }
            });
        });
        
        lazyImages.forEach(image => {
            imageObserver.observe(image);
        });
    }
}

// Añadir estilos para animaciones
const style = document.createElement('style');
style.textContent = `
    @keyframes be-mahou-float-up {
        0% {
            transform: translateY(0) scale(1);
            opacity: 1;
        }
        50% {
            opacity: 0.8;
        }
        100% {
            transform: translateY(-50px) scale(1.5);
            opacity: 0;
        }
    }
    
    .be-mahou-like-effect {
        pointer-events: none;
        animation: be-mahou-float-up 1s ease-out forwards;
    }
`;
document.head.appendChild(style);