let stopWatchSeconds = 0;
let stopStopwatchInterval;
let savedTimeInterval;

function startStopwatch() {
    stopStopwatchInterval = setInterval(function() {
        stopWatchSeconds ++;
        console.log("Elapsed time " + stopWatchSeconds + "s");
    }, 1000);
    savedTimeInterval = setInterval(async function () {
        await saveTime(stopWatchSeconds);
    
    }, 5000)
}

function stopStopwatch() {
    clearInterval(stopStopwatchInterval);
    clearInterval(savedTimeInterval)
    stopWatchSeconds = 0;
}

function saveTime(saveTime) {
    return new Promise(function (resolve, reject) {
        console.log("Saved time " + saveTime + "s");
        resolve()
    })
}