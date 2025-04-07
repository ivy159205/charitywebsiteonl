const campaign

$(document).ready(function () {
  fetchTopDonors();
});

function fetchTopDonors() {
  $.ajax({
      url: "https://localhost:7201/Volunteer/TopDonors?top=3",
      type: "GET",
      dataType: "json",
      success: function (donors) {
          let volunteerList = $("#volunteerList");
          volunteerList.empty(); // Xóa nội dung cũ

          console.log("API Response:", donors); // Kiểm tra dữ liệu trả về

          if (!Array.isArray(donors) || donors.length === 0) {
              volunteerList.html("<p class='text-center'>Không có tình nguyện viên nổi bật.</p>");
              return;
          }

          donors.forEach((donor, index) => {
              let userName = donor.userInfo ? donor.userInfo.uName : "Ẩn danh";
              let donatedAmount = donor.totalDonatedAmount || 0;

              let volunteerHTML = `
          <div class="col-lg-4 col-md-6">
              <div class="single_volenteer">
                  <div class="volenteer_thumb">
                      <img src="img/volenteer/${index + 1}.png" alt="">
                  </div>
                  <div class="voolenteer_info d-flex align-items-end">
                      <div class="social_links">
                          <ul>
                              <li><a href="#"><i class="fa fa-facebook"></i></a></li>
                              <li><a href="#"><i class="fa fa-pinterest"></i></a></li>
                              <li><a href="#"><i class="fa fa-linkedin"></i></a></li>
                              <li><a href="#"><i class="fa fa-twitter"></i></a></li>
                          </ul>
                      </div>
                      <div class="info_inner">
                          <h4>${userName}</h4>
                          <p>Donated: ${donatedAmount.toLocaleString()} VND</p>
                      </div>
                  </div>
              </div>
          </div>
      `;
              volunteerList.append(volunteerHTML);
          });
      },
      error: function () {
          console.error("Lỗi khi lấy dữ liệu từ API");
      }
  });
}