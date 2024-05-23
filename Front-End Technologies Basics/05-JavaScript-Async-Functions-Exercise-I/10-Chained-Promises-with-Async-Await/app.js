function delay(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }
  
  async function chainedPromisesAsync() {
    const result1 = await delay(1000)
    .then(() => 'Result from 1 second delay');
    console.log(result1);
  
    const result2 = await delay(2000)
    .then(() => 'Result from 2 second delay');
    console.log(result2);
  
    const result3 = await delay(3000)
    .then(() => 'Result from 3 second delay');
    console.log(result3);
  }
  
  