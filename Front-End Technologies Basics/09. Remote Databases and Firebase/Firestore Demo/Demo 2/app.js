// Unique Firebase Object
const firebaseConfig = {
    apiKey: "AIzaSyC_OqEhVCBlcUF5nq1xqrJm4H-gwt_YSx4",
    authDomain: "responsivefromdemoproject.firebaseapp.com",
    projectId: "responsivefromdemoproject",
    storageBucket: "responsivefromdemoproject.appspot.com",
    messagingSenderId: "1099350819977",
    appId: "1:1099350819977:web:eb640fa98114af8bc9f258",
    measurementId: "G-CZ0L7P2ER2"
  };

// Initialize Firebase
firebase.initializeApp(firebaseConfig);
const firestore = firebase.firestore();

// Variable to acces database collection
const db = firestore.collection('ResponsiveForm');

// Get Submit Form
let submitButton = document.getElementById('submit');

// Add Event Listener to Enable Form Submission
submitButton.addEventListener('click', (e) => {
    // Prevent Default Form Submission
    e.preventDefault();

    // Get Form Values
    let firstName = document.getElementById('fname').value;
    let lastName = document.getElementById('lname').value;
    let email = document.getElementById('email').value;
    let country = document.getElementById('country-input').value;

    // Save Form Data to Firebase
    db.doc().set({
        fname: firstName,
        lname: lastName,
        email: email,
        country: country
    }).then(() => {
        console.log('Data saved');
    }).catch((error) => {
        console.log(error);
    })

    // alert
    alert('Form submitted successfully');
});

// Function to fetch and display records
function fetchRecords() {
    const recordsList = document.getElementById('records-list');
    recordsList.innerHTML = ''; // clear previous records

    db.get().then((querySnapshot) => {
        querySnapshot.forEach((doc) => {
            const record = doc.data();
            const listItem = document.createElement('li');
            listItem.textContent = `First Name: ${record.fname}, Last Name: ${record.lname}, Email: ${record.email}, Country: ${record.country}`;
            recordsList.appendChild(listItem);
        });
    }).catch((error) => {
        console.log('Error while getting documents: ', error);
    });
}

let getUsersButton = document.getElementById('getRecords');
getUsersButton.addEventListener('click', fetchRecords);

// Make fetchRecords globally accessible
window.fetchRecords = fetchRecords;