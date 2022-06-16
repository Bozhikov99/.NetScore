let playerCardElements = document.querySelectorAll('.player-card');
let modalContainerElement = document.querySelector('.modal-container');
let closeElement = document.querySelector('#close');
let plusElements = modalContainerElement.querySelectorAll('.add');
let minusElements = modalContainerElement.querySelectorAll('.substract');
let awayScore = document.querySelector('#away-goals');
let homeScore = document.querySelector('#home-goals');
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

    //check for goals
    if (currentStat == 'Goals') {
        if (playerNumber < 11) {
            let currentScore = Number(homeScore.textContent);
            homeScore.textContent = currentScore - 1;
            resetAssists('home');
        } else {
            let currentScore = Number(awayScore.textContent);
            awayScore.textContent = currentScore - 1;
            resetAssists('away');
        }

    }

    let card = Array.from(document.querySelectorAll('.player-card'))
        .find(c => c.dataset.player == playerNumber);

    console.log(card);

    let currentInput = card.querySelector(`input[name="${currentStat}"]`)
    console.log(currentInput);
    insertStat(modalStatElement, playerNumber, number);
}

function increaseStat(playerNumber, statName, modalStatElement) {

    let number = Number(modalStatElement.textContent) + 1;

    //check for goals
    if (statName == 'Goals') {
        if (playerNumber < 11) {
            let currentScore = Number(homeScore.textContent);
            homeScore.textContent = currentScore + 1;
        } else {
            let currentScore = Number(awayScore.textContent);
            awayScore.textContent = currentScore + 1;
        }
        modalStatElement.textContent = number;
        insertStat(modalStatElement, playerNumber, number);
    } else if (statName == 'Assists') {
        if (playerNumber <= 11) {
            let currentScore = Number(homeScore.textContent);
            let totalHomeAssists = 0;
            let homeElement = document.querySelector('.home');
            let homeAssistElements = Array.from(homeElement.querySelectorAll('input[name="Assists"]'));
            homeAssistElements.forEach(hae => totalHomeAssists += Number(hae.value));

            if (currentScore > totalHomeAssists) {
                modalStatElement.textContent = number;
                insertStat(modalStatElement, playerNumber, number);
            }
        } else {
            let currentScore = Number(awayScore.textContent);

            if (currentScore >= number) {
                modalStatElement.textContent = number;
                insertStat(modalStatElement, playerNumber, number);
            }
        }
    } else {
        modalStatElement.textContent = number;
        insertStat(modalStatElement, playerNumber, number);
    }

    let card = Array.from(document.querySelectorAll('.player-card'))
        .find(c => c.dataset.player == playerNumber);

    let currentInput = card.querySelector(`input[name="${statName}"]`)
}

function insertStat(statName, playerNumber, value) {
    let stat = statName.dataset.stat;
    let cards = Array.from(document.querySelectorAll('.player-card'));
    let currentCard = cards.find(c => c.dataset.player == playerNumber);
    let targetInput = currentCard.querySelector(`input[name="${stat}"]`)
    console.log(targetInput);
    console.log(targetInput.value);
    targetInput.value = value;
}

function resetAssists(team) {
    let modalAssistsElement = modalContainerElement.querySelector('p[name="Assists"]');
    modalAssistsElement.textContent = 0;
    let teamAssistElements = Array.from(document.querySelectorAll(`.${team}, input[name="Assists"]`));
    teamAssistElements.forEach(i => i.value = 0);
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
    let savesInputElement = pce.querySelector('input[name="Saves"]');
    console.log(goalsInputElement.value);

    //load the stats in the modal
    let goalsElement = modalContainerElement.querySelector('p[name="Goals"]');
    let assistsElement = modalContainerElement.querySelector('p[name="Assists"]');
    let passsesElement = modalContainerElement.querySelector('p[name="Passes"]');
    let tacklesElement = modalContainerElement.querySelector('p[name="Tackles"]');
    let foulsElement = modalContainerElement.querySelector('p[name="Fouls"]');
    let savesElement = modalContainerElement.querySelector('p[name="Saves"]');

    goalsElement.textContent = goalsInputElement.value;
    assistsElement.textContent = assistsInputElement.value;
    passsesElement.textContent = passesInputElement.value;
    tacklesElement.textContent = tacklesInputElement.value;
    foulsElement.textContent = foulsInputElement.value;
    savesElement.textContent = savesInputElement.value;

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
