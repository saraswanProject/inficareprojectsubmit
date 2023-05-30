// login.js

const loginForm = document.getElementById('loginForm');
const messageContainer = document.getElementById('messageContainer');
const getCustomerListButton = document.getElementById('getCustomerListButton');
const getBankListButton = document.getElementById('getBankListButton');
const tokenContainer = document.getElementById('tokenContainer');
let authToken = '';

loginForm.addEventListener('submit', function (event) {
    event.preventDefault(); // Prevent form submission

    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;
    debugger
    const loginData = {
        username: username,
        password: password
    };

    fetch('/api/inficare/authenticate', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(loginData)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Authentication failed');
            }
            return response.json();
        })
        .then(data => {
            if (data.recordStatus == 400) {
                messageContainer.textContent = data.token;

            }
            else if (data.recordStatus == 200) {
                authToken = data.token;
                tokenContainer.textContent = authToken;
                messageContainer.textContent = "Authentication Successfully";
                getCustomerListButton.disabled = false;
                getBankListButton.disabled = false;
            }
            //messageContainer.value = data.recordStatus;
        })
        .catch(error => {
            messageContainer.textContent = 'An error occurred during authentication.';
            console.error(error);
        });
});

getCustomerListButton.addEventListener('click', function () {
    debugger
    fetch('/api/inficare/GetCustomerList', {
        headers: {
            'Authorization': `Bearer ${authToken}`
        }
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to retrieve customer list');
            }
            return response.json();
        })
        .then(data => {
            sessionStorage.setItem('customerList', JSON.stringify(data));
            console.log(data)
            // Redirect to the target page
            window.location.href = '/customerList.html';
        })
        .catch(error => {
            console.error(error);
        });
});

getBankListButton.addEventListener('click', function () {
    fetch('/api/inficare/GetBankList', {
        headers: {
            'Authorization': `Bearer ${authToken}`
        }
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to retrieve bank list');
            }
            return response.json();
        })
        .then(data => {
            // Handle the bank list data
            console.log(data);
        })
        .catch(error => {
            console.error(error);
        });
});