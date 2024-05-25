document.getElementById("showPassword").addEventListener("click", function () {
  togglePasswordVisibility("password", "showPassword");
});

document
  .getElementById("showConfirmPassword")
  .addEventListener("click", function () {
    togglePasswordVisibility("confirmPassword", "showConfirmPassword");
  });

document
  .getElementById("signupForm")
  .addEventListener("submit", function (event) {
    event.preventDefault(); // Prevent default form submission

    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;
    var confirmPassword = document.getElementById("confirmPassword").value;
    var errorDiv = document.getElementById("errorDiv");

    errorDiv.innerHTML = ""; // Clear previous errors
    errorDiv.style.display = "none"; // Hide errorDiv initially

    if (!username || !password || !confirmPassword) {
      displayError("Please enter both username and password.");
      return;
    }

    if (password !== confirmPassword) {
      displayError("Passwords do not match.");
      return;
    }

    if (password.length < 8) {
      displayError("Password must be at least 8 characters long.");
      return;
    }

    // Additional validation logic can be added here

    // If validation passes, submit the form
    this.submit();
  });

function displayError(Message) {
  var errorMessage = document.createElement("div");
  errorMessage.classList.add("alert", "alert-danger");
  errorMessage.textContent = Message;
  errorDiv.appendChild(errorMessage);

  errorDiv.style.display = "block"; // Make errorDiv visible
  return;
}

function togglePasswordVisibility(fieldId, eyeIconId) {
  var passwordField = document.getElementById(fieldId);
  var eyeIcon = document.getElementById(eyeIconId);

  if (passwordField.type === "password") {
    passwordField.type = "text";
    eyeIcon.innerHTML = '<i class="bi bi-eye-slash"></i>';
  } else {
    passwordField.type = "password";
    eyeIcon.innerHTML = '<i class="bi bi-eye"></i>';
  }
}
