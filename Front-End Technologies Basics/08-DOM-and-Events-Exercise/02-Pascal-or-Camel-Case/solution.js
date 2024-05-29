function solve() {
  let textInput = document.getElementById("text").value.toLowerCase();
  let nameConvention = document.getElementById("naming-convention").value;
  let resultField = document.getElementById("result");
  let splitedText = textInput.split(' ');

  let resultText = '';
  if (nameConvention == "Camel Case") {
    resultText += splitedText[0];
    for (let i = 1; i < splitedText.length; i++) {
      resultField += splitedText[i][0].toUpperCase() + splitedText[i].slice(1, splitedText[i].length);
    }
    resultField.textContent = resultText;
  }
  else if (nameConvention == "Pascal Case") {
    resultText += splitedText[0][0].toUpperCase() + splitedText[0].slice(1, splitedText[0].length);
    for (let i = 0; i < splitedText.length; i++) {
      resultField += splitedText[i][0].toUpperCase() + splitedText[i].slice(1, splitedText[i].length);
    }
    resultField.textContent = resultText;
  }
  else {
    resultField.textContent = "Error!"
  }
}