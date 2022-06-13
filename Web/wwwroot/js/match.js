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
    let modalImageElement = modalContainerElement.querySelector('img');
    let modalNameElement = modalContainerElement.querySelector('#modal-name');

    let playerNumber = pce.dataset.player;

    //load the player's match stats
    let goalsInputElement = pce.querySelector('input[name="Goals"]');
    let assistsInputElement = pce.querySelector('input[name="Assists"]');
    let passesInputElement = pce.querySelector('input[name="Passes"]');
    let tacklesInputElement = pce.querySelector('input[name="Tackles"]');
    let foulsInputElement = pce.querySelector('input[name="Fouls"]');
    console.log(goalsInputElement.value);

    //load the stats in the modal
    let goalsElement = modalContainerElement.querySelector('p[name="Goals"]');
    let assistsElement = modalContainerElement.querySelector('p[name="Assists"]');
    let passsesElement = modalContainerElement.querySelector('p[name="Passes"]');
    let tacklesElement = modalContainerElement.querySelector('p[name="Tackles"]');
    let foulsElement = modalContainerElement.querySelector('p[name="Fouls"]');

    goalsElement.textContent = goalsInputElement.value;
    assistsElement.textContent = assistsInputElement.value;
    passsesElement.textContent = passesInputElement.value;
    tacklesElement.textContent = tacklesInputElement.value;
    foulsElement.textContent = foulsInputElement.value;

    //load the player's image
    let playerImageElement = pce.querySelector('img');
    let playerImageSrc = playerImageElement.src;
    modalImageElement.src = playerImageSrc;

    //load the player's name
    let playerNameElement = pce.querySelector('h3');
    let playerLastName = playerNameElement.textContent;
    let playerFirstName = playerNameElement.dataset.firstname;
    modalNameElement.textContent = `${playerFirstName} ${playerLastName}`;

    let plusElements = modalContainerElement.querySelectorAll('.add');
    let minusElements = modalContainerElement.querySelectorAll('.substract');

    plusElements.forEach(pe => pe.addEventListener('click', () => {
        let parentElement = pe.parentElement;
        let modalStatElement = parentElement.querySelector('.stat');
        let number = Number(modalStatElement.textContent) + 1;
        modalStatElement.textContent = number;
    }));

    minusElements.forEach(me => me.addEventListener('click', () => {
        let parentElement = me.parentElement;
        let modalStatElement = parentElement.querySelector('.stat');
        let currentStat = Number(modalStatElement.textContent);

        if (currentStat) {
            let number = Number(modalStatElement.textContent) - 1;
            modalStatElement.textContent = number;
        }

    }))

    modalContainerElement.removeAttribute('hidden');
}));