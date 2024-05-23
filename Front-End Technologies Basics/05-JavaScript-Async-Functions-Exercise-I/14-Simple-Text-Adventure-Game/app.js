
document.querySelector('button').addEventListener('click', startGame);

function delay(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

async function showMessage(message) {
    const gameArea = document.createElement('div');
    gameArea.innerHTML = `<p>${message}</p>`;
    document.body.appendChild(gameArea);
    await delay(1000);
}

async function getUserInput(question) {
    return new Promise(resolve => {
        const inputArea = document.createElement('div');
        inputArea.innerHTML = `<p>${question}</p><input type="text" id="user-input"><button id="submit-button">Submit</button>`;
        document.body.appendChild(inputArea);

        document.getElementById('submit-button').onclick = () => {
            const userInput = document.getElementById('user-input').value.toLowerCase();
            resolve(userInput);
            inputArea.remove();
        };
    });
}

async function startGame() {
    document.body.innerHTML = '<button>Start Adventure</button>'; // Reset the game area
    document.querySelector('button').addEventListener('click', startGame);

    await showMessage("Welcome to the simple text adventure game!");

    let choice = await getUserInput("You find yourself in a dark forest. You can go 'left' or 'right'. What do you do? (left/right): ");
    if (choice === "left") {
        await showMessage("You encounter a wild animal! You can 'fight' or 'run'.");
        choice = await getUserInput("What do you do? (fight/run): ");
        if (choice === "fight") {
            await showMessage("You bravely fight the animal and win!");
        } else {
            await showMessage("You run away safely.");
        }
    } else if (choice === "right") {
        await showMessage("You find a treasure chest! You can 'open' it or 'leave' it.");
        choice = await getUserInput("What do you do? (open/leave): ");
        if (choice === "open") {
            await showMessage("You open the chest and find a treasure! You win!");
        } else {
            await showMessage("You leave the chest and go back to the start.");
            return startGame();
        }
    } else {
        await showMessage("Invalid choice. Please start again.");
        return startGame();
    }

    choice = await getUserInput("Do you want to play again? (yes/no): ");
    if (choice === "yes") {
        startGame();
    } else {
        await showMessage("Thank you for playing!");
    }
}
