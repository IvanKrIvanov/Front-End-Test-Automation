window.addEventListener("load", solve);

function solve() {
    const ticketsNum = document.getElementById('num-tickets');
    const preference = document.getElementById('seating-preference');
    const fullName = document.getElementById('full-name');
    const email = document.getElementById('email');
    const phoneNum = document.getElementById('phone-number');
    const purchaseBtn = document.getElementById('purchase-btn');
    const ticketPreviewList = document.getElementById('ticket-preview');
    const ticketPurchaseList = document.getElementById('ticket-purchase');

    purchaseBtn.addEventListener('click', onPurchase);


    function onPurchase(e) {
        e.preventDefault();

        if (ticketsNum.value === '' || preference.value === '' || fullName.value === '' || email.value === '' || phoneNum.value === '') {
            return;
        }

        let li = document.createElement('li');
        li.className = 'ticket-content';

        let article = document.createElement('article');

        let p1 = document.createElement('p');
        p1.textContent = `Tickets: ${ticketsNum.value}`;
        
        let p2 = document.createElement('p');
        p2.textContent = `Preference: ${preference.value}`;
        
        let p3 = document.createElement('p');
        p3.textContent = `Name: ${fullName.value}`;
        
        let p4 = document.createElement('p');
        p4.textContent = `Email: ${email.value}`;
        
        let p5 = document.createElement('p');
        p5.textContent = `Phone: ${phoneNum.value}`;

        article.appendChild(p1);
        article.appendChild(p2);
        article.appendChild(p3);
        article.appendChild(p4);
        article.appendChild(p5);

        li.appendChild(article);

        let editBtn = document.createElement('button');
        editBtn.className = 'edit-btn';
        editBtn.textContent = 'Edit';
        editBtn.addEventListener('click', onEdit);

        let nextBtn = document.createElement('button');
        nextBtn.className = 'next-btn';
        nextBtn.textContent = 'Next';
        nextBtn.addEventListener('click', onNext);

        li.appendChild(editBtn);
        li.appendChild(nextBtn);

        ticketPreviewList.appendChild(li);

        let editedNumTickets = ticketsNum.value;
        let editedPreference = preference.value;
        let editedFullName = fullName.value;
        let editedEmail = email.value;
        let editedPhoneNumber = phoneNum.value;

        ticketsNum.value = '';
        preference.value = '';
        fullName.value = '';
        email.value = '';
        phoneNum.value = '';

        purchaseBtn.disabled = true;

        function onEdit() {
            ticketsNum.value = editedNumTickets;
            preference.value = editedPreference;
            fullName.value = editedFullName;
            email.value = editedEmail;
            phoneNum.value = editedPhoneNumber;

            li.remove();
            purchaseBtn.disabled = false;
        }

        function onNext() {
            let purchaseLi = document.createElement('li');
            purchaseLi.className = 'ticket-content';
            purchaseLi.appendChild(article);

            let buyBtn = document.createElement('button');
            buyBtn.className = 'buy-btn';
            buyBtn.textContent = 'Buy';
            buyBtn.addEventListener('click', onBuy);

            purchaseLi.appendChild(buyBtn);

            li.remove();
            ticketPurchaseList.appendChild(purchaseLi);
        }

        function onBuy() {
            ticketPurchaseList.innerHTML = '';
            let thankYouMsg = document.createElement('h2');
            thankYouMsg.textContent = 'Thank you for your purchase!';
            document.body.appendChild(thankYouMsg);

            let backBtn = document.createElement('button');
            backBtn.textContent = 'Back';
            backBtn.addEventListener('click', onBack);
            document.body.appendChild(backBtn);

            function onBack() {
                thankYouMsg.remove();
                backBtn.remove();
                purchaseBtn.disabled = false;
            }
        }
    }
}