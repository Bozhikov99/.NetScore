let selectElements = document.querySelectorAll('select');
let clearHomeElements = document.querySelectorAll('.clear-home');
let clearAwayElements = document.querySelectorAll('.clear-away');
let optionElements = Array.from(document.querySelectorAll('option'));
let selectedValues = [];

selectElements.forEach(se => se.addEventListener('change', () => {
    let value = se.value;
    selectedValues.push(value);

    let selectedOptionElements = optionElements.filter(x => selectedValues.includes(x.value));
    selectedOptionElements.forEach(sop => sop.setAttribute('disabled', 'true'));

    console.log(`changed to ${value}`);
}));

clearAwayElements.forEach(ca => ca.addEventListener('click', () => {
    let fixtureDivElement = ca.parentElement;
    let selectElement = fixtureDivElement.querySelector('#AwayTeamId');
    let value = selectElement.value;
    selectElement.selectedIndex = 0;

    clearOptions(value);
}))

clearHomeElements.forEach(ch => ch.addEventListener('click', () => {

    let fixtureDivElement = ch.parentElement;
    let selectElement = fixtureDivElement.querySelector('#HomeTeamId');
    let value = selectElement.value;
    selectElement.selectedIndex = 0;

    clearOptions(value);
}))

function clearOptions(value) {
    selectedValues.pop(value);

    optionElements
        .filter(oe => oe.value == value)
        .forEach(oe => oe.removeAttribute('disabled'));
}