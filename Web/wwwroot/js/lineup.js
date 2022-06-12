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
    let homeCheckedTotal = homeCheckboxes.filter(c => c.checked);
    let awayCheckedTotal = awayCheckboxes.filter(c => c.checked);

    let homeGks = homeCheckboxes.filter(hc => isPositionChecked(hc, 'goalkeeper'));
    let awayGks = awayCheckboxes.filter(hc => isPositionChecked(hc, 'goalkeeper'));

    let homeDefenders = homeCheckboxes.filter(hc => isPositionChecked(hc, 'defender'));
    let awayDefenders = awayCheckboxes.filter(hc => isPositionChecked(hc, 'defender'));

    let homeMidfielders = homeCheckboxes.filter(hc => isPositionChecked(hc, 'midfielder'));
    let awayMidfielders = awayCheckboxes.filter(hc => isPositionChecked(hc, 'midfielder'));

    let homeStrikers = homeCheckboxes.filter(hc => isPositionChecked(hc, 'striker'));
    let awayStrikers = awayCheckboxes.filter(hc => isPositionChecked(hc, 'midfielder'));

    let isAwayCountValid = awayCheckedTotal.length == 11
    let isHomeCountValid = homeCheckedTotal.length == 11
    //let isAway
    //    &&
    //    awayGk == 1 &&
    //    awayDefenders >= 3 &&
    //    awayMidfielders >= 2 &&
    //    awayStrikers >= 1

    let isHomeSquadValid =
        homeGks.length == 1 &&
        homeDefenders.length >= 3 &&
        homeMidfielders.length >= 2 &&
        homeStrikers.length >= 1;

    let isAwaySquadValid =
        awayGks.length == 1 &&
        awayDefenders.length >= 3 &&
        awayMidfielders.length >= 2 &&
        awayStrikers.length >= 1;

    let areTeamsValid = isAwaySquadValid && isHomeSquadValid;
    let areCountsValid = isHomeCountValid && isAwayCountValid;

    if (areCountsValid && areTeamsValid) {
        submitButtonElement.removeAttribute('disabled');
    } else {
        console.log('Home and Away starting players must be 11')
        submitButtonElement.setAttribute('disabled', 'true');
    }
}

function isPositionChecked(element, className) {
    return element.classList.contains(className) &&
        element.checked;
}
