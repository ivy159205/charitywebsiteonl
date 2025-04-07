const campaignUrl = 'http://vanesline.runasp.net/api/Campaign';
const imageUrl = 'http://vanesline.runasp.net/CampaignMedia';

document.addEventListener("DOMContentLoaded", function () {
    loadCampaignData();
});

function loadCampaignData() {
    fetch(`${campaignUrl}/GetList`)
        .then(response => response.json())
        .then(campaigns => {
            const topCampaigns = campaigns.slice(0, 3); // Lấy 5 chiến dịch đầu tiên
            loadCampaignImages(topCampaigns);
        })
        .catch(error => {
            alert('Lỗi khi tải dữ liệu chiến dịch');
            console.error(error);
        });
}

function loadCampaignImages(campaigns) {
    fetch(`${imageUrl}/GetList`)
        .then(response => response.json())
        .then(images => {
            renderCampaignsInSlider(campaigns, images);
        })
        .catch(error => {
            alert('Lỗi khi tải hình ảnh chiến dịch');
            console.error(error);
        });
}

function renderCampaignsInSlider(campaigns, mediaData) {
    const container = document.getElementById("campaignContainer");
    container.innerHTML = ""; // Xóa nội dung cũ

    campaigns.forEach(campaign => {
        const media = mediaData.find(m => m.mCampaignId === campaign.cCampaignId);
        const image = media ? media.mMediaUrl : "img/default.jpg";

        const raised = campaign.cCollectedAmount || 0;
        const goal = campaign.cGoalAmount || 1;
        const progress = (raised / goal) * 100;

        const campaignItem = `
            <div class="single_cause">
                <div class="thumb">
                    <img src="${image}" alt="${campaign.cTitle}" style="height: 273px">
                </div>
                <div class="causes_content">
                    <div class="custom_progress_bar">
                        <div class="progress">
                            <div class="progress-bar" role="progressbar" style="width: ${progress}%"
                                aria-valuenow="${progress}" aria-valuemin="0" aria-valuemax="100">
                                <span class="progres_count">${Math.round(progress)}%</span>
                            </div>
                        </div>
                    </div>
                    <div class="balance d-flex justify-content-between align-items-center">
                        <span>Raised: ${raised.toLocaleString()} VNĐ</span>
                        <span>Goal: ${goal.toLocaleString()} VNĐ</span>
                    </div>
                    <a href="cause_details.html?id=${campaign.cCampaignId}"><h4>${campaign.cTitle}</h4></a>
                    <p>${campaign.cDescription || 'No description available'}</p>
                    <a class="read_more" href="cause_details.html?id=${campaign.cCampaignId}">Read More</a>
                </div>
            </div>
        `;

        container.innerHTML += campaignItem;
    });

    // Khởi tạo Owl Carousel sau khi render xong
    $("#campaignContainer").owlCarousel({
        items: 1, // một slide chứa 5 dự án
        loop: false,
        margin: 10,
        nav: true,
        dots: true,
    });
}
