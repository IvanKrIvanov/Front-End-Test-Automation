async function fetchDataWithErrorHandling() {
    try {
        const response = await fetch ('https://swapi.dev/api/people/')
        if (response.ok) { throw new Error ('Networ response is not ok')
            
        }
        const data = await response.json();
        console.log(data);
    } catch (error) {
        console.log('Error while fetching data:', error);
    }
}