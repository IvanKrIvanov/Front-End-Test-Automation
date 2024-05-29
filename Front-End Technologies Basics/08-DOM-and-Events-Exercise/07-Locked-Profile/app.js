function lockedProfile() {
    let buttons = document.getElementsByTagName("button");

    for (let i = 0; i < buttons.length; i++) {
        buttons[i].addEventListener('click', showInfo)
    }

    function showInfo(event) {
        let lockRadioButton = event.target.parentNode.children[2];
        let divHiddenContext = event.target.previousElementSibling;
        console.log(divHiddenContext);

        if (lockRadioButton.checked == false) {
            if (event.target.textContent == 'Hide it') {
                divHiddenContext.style.display = 'none'
                event.target.textContent = 'Show more'
            }
            else {
                divHiddenContext.style.display = 'inline'
                event.target.textContent = 'Hide it'
            }
        }
    }
}