document.addEventListener("DOMContentLoaded", function () {
  setTimeout(() => {
      let swiperEl = document.querySelector(".bradcam_swiper");
      if (!swiperEl || swiperEl.offsetWidth === 0 || swiperEl.offsetHeight === 0) {
          console.error("⚠️ Swiper chưa có kích thước hợp lệ, hoãn khởi tạo.");
          return;
      }
      
      console.log("Khởi tạo Swiper...");
      new Swiper(".bradcam_swiper", {
          loop: true,
          autoplay: { delay: 8000, disableOnInteraction: false },
          pagination: { el: ".swiper-pagination", clickable: true }
      });
  }, 500); // Đợi 500ms xem có thay đổi không
});
