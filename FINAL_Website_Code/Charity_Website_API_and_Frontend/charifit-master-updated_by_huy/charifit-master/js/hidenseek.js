document.getElementById('logoutBtn').addEventListener('click', function () {
    localStorage.removeItem('userData');
    window.location.href = 'index.html';
});

window.onload = function () {
    const userData = JSON.parse(localStorage.getItem('userData'));

    if (userData) {
        document.getElementById('loginBtn').style.display = 'none';
        document.getElementById('logoutBtn').style.display = 'block';
    } else {
        document.getElementById('loginBtn').style.display = 'block';
        document.getElementById('logoutBtn').style.display = 'none';
    }
};