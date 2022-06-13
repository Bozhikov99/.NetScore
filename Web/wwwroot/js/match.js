let playerCardElements = document.querySelectorAll('.player-card');
let modalContainerElement = document.querySelector('.modal-container');
let closeElement = document.querySelector('#close');
let plusElements = modalContainerElement.querySelectorAll('.add');
let minusElements = modalContainerElement.querySelectorAll('.substract');
let playerNumber = 0;

plusElements.forEach(pe => pe.addEventListener('click', () => {
    let parentElement = pe.parentElement;
    let modalStatElement = parentElement.querySelector('.stat');
    let statName = modalStatElement.dataset.stat;
    let number = Number(modalStatElement.textContent);
    modalStatElement.textContent = number;
    increaseStat(playerNumber, statName, modalStatElement);
}));

minusElements.forEach(me => me.addEventListener('click', () => {
    let parentElement = me.parentElement;
    let modalStatElement = parentElement.querySelector('.stat');
    let statName = modalStatElement.dataset.stat;
    let currentStat = Number(modalStatElement.textContent);

    if (currentStat) {
        decreaseStat(playerNumber, statName, modalStatElement);
    }
}));

function decreaseStat(playerNumber, currentStat, modalStatElement) {
    let number = Number(modalStatElement.textContent) - 1;
    modalStatElement.textContent = number;

    let card = Array.from(document.querySelectorAll('.player-card'))
        .find(c => c.dataset.player == playerNumber);

    console.log(card);

    let currentInput = card.querySelector(`input[name="${currentStat}"]`)
    console.log(currentInput);
    insertStat(modalStatElement, playerNumber, number);
}

function increaseStat(playerNumber, statName, modalStatElement) {

    let number = Number(modalStatElement.textContent) + 1;
    modalStatElement.textContent = number;

    let card = Array.from(document.querySelectorAll('.player-card'))
        .find(c => c.dataset.player == playerNumber);

    console.log(card);

    let currentInput = card.querySelector(`input[name="${statName}"]`)
    console.log(currentInput);
    insertStat(modalStatElement, playerNumber, number);
}

function insertStat(statName, playerNumber, value) {
    let cards = Array.from(document.querySelectorAll('.player-card'));
    let currentCard = cards.find(c => c.dataset.player == playerNumber);
    let targetInput = currentCard.querySelector(`input[name="${statName.dataset.stat}"]`)
    console.log(targetInput);
    console.log(targetInput.value);
    targetInput.value = value;
}

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

    playerNumber = pce.dataset.player;

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

    plusElements.forEach(pe => {
        let newNode = pe.cloneNode()
        pe = newNode;
    });

    minusElements.forEach(me => {
        let newNode = me.cloneNode()
        me = newNode;
    });

    modalContainerElement.removeAttribute('hidden');
}))
