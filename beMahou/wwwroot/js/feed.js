// Funcionalidad de likes
document.addEventListener('DOMContentLoaded', function() {
    // Configurar botones de like
    const likeButtons = document.querySelectorAll('.be-mahou-like-btn');
    
    likeButtons.forEach(button => {
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
                    this.style.transform = 'scale(1.1)';
                    setTimeout(() => {
                        this.style.transform = '';
                    }, 200);
                    
                    // Efecto de partículas (opcional)
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
    
    // Efecto al hacer clic en cualquier acción
    const actionButtons = document.querySelectorAll('.be-mahou-action-btn');
    actionButtons.forEach(button => {
        button.addEventListener('click', function() {
            this.style.transform = 'scale(0.95)';
            setTimeout(() => {
                this.style.transform = '';
            }, 200);
        });
    });
});

// Efecto visual para likes (opcional)
function createLikeEffect(element) {
    const effect = document.createElement('div');
    effect.className = 'be-mahou-like-effect';
    effect.innerHTML = '<i class="fas fa-star"></i>';
    
    // Posicionamiento
    const rect = element.getBoundingClientRect();
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

// Animación para el efecto de like
const style = document.createElement('style');
style.textContent = `
    @keyframes be-mahou-float-up {
        0% {
            transform: translateY(0) scale(1);
            opacity: 1;
        }
        100% {
            transform: translateY(-50px) scale(1.5);
            opacity: 0;
        }
    }
`;
document.head.appendChild(style);