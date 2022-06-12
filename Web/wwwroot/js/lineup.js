let submitButtonElement = document.querySelector('button[type="submit"]');
let checkboxElements = document.querySelectorAll('input');
submitButtonElement.setAttribute('disabled', 'true');

checkboxElements.forEach(cbe => cbe.addEventListener(
    'change', () => {
        let homeCheckboxElements = Array.from(document.querySelectorAll('.home input'));
        let awayCheckboxElements = Array.from(document.querySelectorAll('.away input'));
        checkStartingPLayers(homeCheckboxElements, awayCheckboxElements);

    }));



function checkStartingPLayers(homeCheckboxes, awayCheckboxes) {
    let homeChecked = homeCheckboxes.filter(c => c.checked);
    let awayChecked = awayCheckboxes.filter(c => c.checked);

    if (awayChecked.length == 11 && homeChecked.length == 11) {
        submitButtonElement.removeAttribute('disabled');
    }
    else {
        submitButtonElement.setAttribute('disabled', 'true');
    }
}