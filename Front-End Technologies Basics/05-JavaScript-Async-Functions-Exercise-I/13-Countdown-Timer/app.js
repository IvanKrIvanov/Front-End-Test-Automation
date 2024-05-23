document.getElementById('startButton').addEventListener('click', function() {
    let n = parseInt(document.getElementById('countdownInput').value);
    if (isNaN(n) || n <= 0) {
        console.log("Please enter a valid number of seconds.");
        return;
    }

    console.log(`Countdown started for ${n} seconds.`);
    
    let countdown = setInterval(() => {
        console.log(`Remaining time: ${n} seconds`);
        n--;

        if (n < 0) {
            clearInterval(countdown);
            console.log("Countdown finished.");
            simulateSaveRemainingTime(0);
        }
    }, 1000);
});

function simulateSaveRemainingTime(remainingTime) {
    console.log("Saving remaining time...");
    // Simulate an asynchronous save operation
    setTimeout(() => {
        console.log(`Remaining time of ${remainingTime} seconds saved successfully.`);
    }, 2000); // Simulate a delay for the asynchronous operation
}
