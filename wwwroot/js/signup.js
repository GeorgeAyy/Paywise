document
  .getElementById("signUpForm")
  .addEventListener("submit", function (event) {
    var isValid = true;

    // Clear previous error messages
    document.getElementById("usernameError").innerText = "";
    document.getElementById("emailError").innerText = "";
    document.getElementById("passwordError").innerText = "";
    document.getElementById("confirmPasswordError").innerText = "";

    // Validate username
    var username = document.getElementById("username").value;
    if (username.length < 3) {
      document.getElementById("usernameError").innerText =
        "Username must be at least 3 characters long";
      isValid = false;
    }

    // Validate email
    var email = document.getElementById("email").value;
    var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailPattern.test(email)) {
      document.getElementById("emailError").innerText =
        "Please enter a valid email address";
      isValid = false;
    }

    // Validate password
    var password = document.getElementById("password").value;
    if (password.length < 6) {
      document.getElementById("passwordError").innerText =
        "Password must be at least 6 characters long";
      isValid = false;
    }

    // Validate confirm password
    var confirmPassword = document.getElementById("confirmPassword").value;
    if (password !== confirmPassword) {
      document.getElementById("confirmPasswordError").innerText =
        "Passwords do not match";
      isValid = false;
    }

    if (!isValid) {
      event.preventDefault(); // Prevent form submission if validation fails
    }
  });

// Toggle password visibility
document
  .getElementById("togglePassword")
  .addEventListener("click", function () {
    var passwordField = document.getElementById("password");
    var type =
      passwordField.getAttribute("type") === "password" ? "text" : "password";
    passwordField.setAttribute("type", type);
    this.classList.toggle("fa-eye-slash");
  });

document
  .getElementById("toggleConfirmPassword")
  .addEventListener("click", function () {
    var confirmPasswordField = document.getElementById("confirmPassword");
    var type =
      confirmPasswordField.getAttribute("type") === "password"
        ? "text"
        : "password";
    confirmPasswordField.setAttribute("type", type);
    this.classList.toggle("fa-eye-slash");
  });
