// controllers/signupController.js
const User = require("../models/userModel.js");
const bcrypt = require("bcrypt");

const registrationProcess = async (req, res) => {
  try {
    const { username, password, confirmPassword } = req.body;

    if (password !== confirmPassword) {
      return res.render("signup", {
        currentPage: "signup",
        error: "Passwords do not match.",
        user: null,
      });
    }

    const existingUser = await User.findOne({ username });
    if (existingUser) {
      return res.render("signup", {
        currentPage: "signup",
        error: "Username already exists.",
        user: null,
      });
    }

    const hashedPassword = await bcrypt.hash(password, 10);
    const newUser = new User({
      username,
      password: hashedPassword,
      // Handle image upload if necessary
    });

    await newUser.save();
    req.session.user = newUser;
    res.redirect("/");
  } catch (error) {
    console.error(error);
    res.status(500).send("Internal Server Error");
  }
};

module.exports = {
  registrationProcess,
};
