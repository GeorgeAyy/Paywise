document.addEventListener("DOMContentLoaded", function () {
  document
    .getElementById("addCategoryForm")
    .addEventListener("submit", function (event) {
      var isValid = true;

      // Clear previous error messages
      document.querySelectorAll(".text-danger").forEach(function (el) {
        el.innerText = "";
      });

      // Validate category name
      var categoryName = document.getElementById("categoryName").value;
      if (!categoryName) {
        document.getElementById("categoryNameError").innerText =
          "Category name is required";
        isValid = false;
      }

      if (!isValid) {
        event.preventDefault(); // Prevent form submission if validation fails
      }
    });
});
