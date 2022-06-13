let playerCardElements = document.querySelectorAll('.player-card');
let modalContainerElement = document.querySelector('.modal-container');
let closeElement = document.querySelector('#close');

closeElement.addEventListener('click', () => {
    modalContainerElement.setAttribute('hidden', true);
});

playerCardElements.forEach(pce => pce.addEventListener('mouseover', () => {
    pce.classList.add('active-card');
}));

playerCardElements.forEach(pce => pce.addEventListener('mouseout', () => {
    pce.classList.remove('active-card');
}));

playerCardElements.forEach(pce => pce.addEventListener('click', () => {
    modalContainerElement.removeAttribute('hidden');
}));