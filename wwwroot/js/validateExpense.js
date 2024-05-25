document.addEventListener("DOMContentLoaded", function () {
  document
    .getElementById("expenseForm")
    .addEventListener("submit", function (event) {
      var isValid = true;

      // Clear previous error messages
      document.querySelectorAll(".text-danger").forEach(function (el) {
        el.innerText = "";
      });

      // Validate description
      var description = document.getElementById("description").value;
      if (!description) {
        document.getElementById("descriptionError").innerText =
          "Description is required";
        isValid = false;
      }

      // Validate amount
      var amount = document.getElementById("amount").value;
      if (!amount || parseFloat(amount) <= 0) {
        document.getElementById("amountError").innerText =
          "Amount must be greater than 0";
        isValid = false;
      }

      // Validate date
      var date = document.getElementById("date").value;
      if (!date) {
        document.getElementById("dateError").innerText = "Date is required";
        isValid = false;
      }

      // Validate category
      var category = document.getElementById("category").value;
      if (!category) {
        document.getElementById("categoryError").innerText =
          "Category is required";
        isValid = false;
      }

      if (!isValid) {
        event.preventDefault(); // Prevent form submission if validation fails
      }
    });
});
