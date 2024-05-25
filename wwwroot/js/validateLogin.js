document.addEventListener("DOMContentLoaded", function () {
  document
    .getElementById("loginForm")
    .addEventListener("submit", function (event) {
      var isValid = true;

      // Clear previous error messages
      document.querySelectorAll(".text-danger").forEach(function (el) {
        el.innerText = "";
      });

      // Validate username
      var username = document.getElementById("username").value;
      if (!username) {
        document.getElementById("usernameError").innerText =
          "Username is required";
        isValid = false;
      }

      // Validate password
      var password = document.getElementById("password").value;
      if (!password) {
        document.getElementById("passwordError").innerText =
          "Password is required";
        isValid = false;
      }

      if (!isValid) {
        event.preventDefault(); // Prevent form submission if validation fails
      }
    });
});
