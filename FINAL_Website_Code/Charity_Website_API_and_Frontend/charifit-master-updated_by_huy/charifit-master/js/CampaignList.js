const campaignUrl = 'http://vanesline.runasp.net/api/Campaign';
const imageUrl = 'http://vanesline.runasp.net/CampaignMedia';
let currentPage = 1;
const itemsPerPage = 9;
let allCampaigns = [];
let allMedia = [];

document.addEventListener("DOMContentLoaded", function () {
    loadCampaignData();
});

function loadCampaignData() {
    fetch(`${campaignUrl}/GetList`)
        .then(response => response.json())
        .then(campaigns => {
            allCampaigns = campaigns;
            loadCampaignImages();
        })
        .catch(error => {
            alert('Lỗi khi tải dữ liệu chiến dịch');
            console.error(error);
        });
}

function loadCampaignImages() {
    fetch(`${imageUrl}/GetList`)
        .then(response => response.json())
        .then(images => {
            allMedia = images;
            renderCampaigns(); // Gọi render sau khi có đủ dữ liệu
            renderPagination(); // Vẽ phân trang
        })
        .catch(error => {
            alert('Lỗi khi tải hình ảnh chiến dịch');
            console.error(error);
        });
}
function renderCampaigns() {
    const campaignContainer = document.getElementById("campaign-list");
    campaignContainer.innerHTML = "";

    const startIndex = (currentPage - 1) * itemsPerPage;
    const endIndex = startIndex + itemsPerPage;
    const campaignsToRender = allCampaigns.slice(startIndex, endIndex);

    campaignsToRender.forEach(campaign => {
        const media = allMedia.find(m => m.mCampaignId === campaign.cCampaignId);
        const image = media ? media.mMediaUrl : "img/default.jpg";

        const raised = campaign.cCollectedAmount || 0;
        const goal = campaign.cGoalAmount || 1;
        const progress = (raised / goal) * 100;

        const campaignElement = `
            <div class="col-lg-4 col-md-6">
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
            </div>
        `;

        campaignContainer.innerHTML += campaignElement;
    });
}

