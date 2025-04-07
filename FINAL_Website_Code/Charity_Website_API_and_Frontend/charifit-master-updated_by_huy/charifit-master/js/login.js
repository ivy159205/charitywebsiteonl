document.getElementById("loginForm").addEventListener("submit", async function (event) {
    event.preventDefault();

    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;

    try {
        const response = await fetch(`http://vanesline.runasp.net/api/GetJWTToken/Login?uid=${encodeURIComponent(email)}&pwd=${encodeURIComponent(password)}`, {
            method: "GET",
            headers: { "Content-Type": "application/json" }
        });

        const data = await response.json();
        console.log("Response từ API:", data); // Kiểm tra API trả về

        if (data.code === 100) {
            // Lưu vào localStorage
            localStorage.setItem("userData", JSON.stringify({
                userId: data.userId,
                role: data.quyen,
                token: data.token
            }));

            alert("Đăng nhập thành công!");

            // Điều hướng dựa vào role
            if (data.quyen === "admin") {
                window.location.href = "admin.html";
            } else {
                window.location.href = "index.html";
            }
        } else {
            document.getElementById("message").innerText = data.msg || "Lỗi đăng nhập!";
        }
    } catch (error) {
        document.getElementById("message").innerText = "Không thể kết nối tới server!";
    }
});

const inputs = document.querySelectorAll(".input");


function addcl() {
    let parent = this.parentNode.parentNode;
    parent.classList.add("focus");
}

function remcl() {
    let parent = this.parentNode.parentNode;
    if (this.value == "") {
        parent.classList.remove("focus");
    }
}


inputs.forEach(input => {
    input.addEventListener("focus", addcl);
    input.addEventListener("blur", remcl);
});