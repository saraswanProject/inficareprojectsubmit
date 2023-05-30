// customerList.js

document.addEventListener('DOMContentLoaded', function () {
    debugger
    const customerListContainer = document.getElementById('customerListContainer');

    // Retrieve the customer list data from the session storage
    const customerListData = sessionStorage.getItem('customerList');
    if (customerListData) {
        const customerList = JSON.parse(customerListData);

        // Render the customer list on the page
        customerList.forEach(customer => {
            const customerElement = document.createElement('div');
            customerElement.textContent = `Customer ID: ${customer.customerId}, Customer Name: ${customer.customerName}`;
            customerListContainer.appendChild(customerElement);
        });
    }
});
