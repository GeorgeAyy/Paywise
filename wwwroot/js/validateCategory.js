document.addEventListener("DOMContentLoaded", function () {
  // Add Category Form Validation
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

  // Event listener for Edit Category Modal
  var editModal = document.getElementById("editCategoryModal");
  editModal.addEventListener("show.bs.modal", function (event) {
    var button = event.relatedTarget;
    var id = button.getAttribute("data-id");
    var name = button.getAttribute("data-name");
    var modal = this;
    modal.querySelector("#editCategoryId").value = id;
    modal.querySelector("#editCategoryName").value = name;
    console.log("Edit modal opened. Category ID: " + id); // Debugging information
  });

  // Event listener for Delete Category Modal
  var deleteModal = document.getElementById("deleteCategoryModal");
  deleteModal.addEventListener("show.bs.modal", function (event) {
    var button = event.relatedTarget;
    var id = button.getAttribute("data-id");
    var modal = this;
    modal.querySelector("#deleteCategoryId").value = id;
    console.log("Delete button clicked. Data-id: " + id); // Debugging information
    console.log("Delete modal opened. Category ID: " + id); // Debugging information
  });
});
