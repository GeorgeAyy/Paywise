// login.js

document.getElementById("showPassword").addEventListener("click", function () {
  var passwordField = document.getElementById("password");
  if (passwordField.type === "password") {
    passwordField.type = "text";
    this.innerHTML = '<i class="bi bi-eye-slash"></i>';
  } else {
    passwordField.type = "password";
    this.innerHTML = '<i class="bi bi-eye"></i>';
  }
});

document
  .getElementById("loginForm")
  .addEventListener("submit", function (event) {
    event.preventDefault(); // Prevent default form submission

    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;
    var errorDiv = document.getElementById("errorDiv");

    errorDiv.innerHTML = ""; // Clear previous errors
    errorDiv.style.display = "none"; // Hide errorDiv initially

    if (!username || !password) {
      // Display error message
      var errorMessage = document.createElement("div");
      errorMessage.classList.add("alert", "alert-danger");
      errorMessage.textContent = "Please enter both username and password.";
      errorDiv.appendChild(errorMessage);

      errorDiv.style.display = "block"; // Make errorDiv visible
      return;
    }

    // Additional validation logic can be added here

    // If validation passes, submit the form
    this.submit();
  });
