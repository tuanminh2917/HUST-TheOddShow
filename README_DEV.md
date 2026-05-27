# DEV

## Các bước khi dùng GitHub kết hợp Unity

**Nếu lần đầu tham gia truy cập git của dự án**

*Với Leader hoặc người quản lý chính*
- Cách 1: Tạo repo local trước rồi liên kết với GitHub  
  ```
  git init //Khởi tạo
  git add. //Thêm tất cả file vào stage
  git commit -m "Initial commit" //Commit lần đầu
  git remote add origin https://github.com/yourusername/yourproject.git //Thêm remote GitHub
  git push -u origin main //Đẩy code lên GitHub
  ```
- Cách 2: Tạo repo trên GitHub trước rồi clone về máy  
  `git clone <repo_url>` //Yêu cầu cần có repo trên GitHub và việc clone đã bao gồm cả git init và git remote.
  
*Với các thành viên khác*
- Tạo 1 clone về máy (cả leader cũng nên tạo nhánh làm việc riêng)  
  `git clone <repo_url>`
- Chuyển sang nhánh làm việc riêng hoặc tạo mới nếu chưa có  
  `git checkout <branch_name>` // chuyển nhánh  
  `git checkout -b <new_brach_name>` // tạo mới nhánh và truy cập

  *Mỗi người nên chỉ làm việc với branch của riêng mình*

**Các lần truy cập sau**
- Sau mỗi lần làm xong và muốn đẩy lên repo của git  
  ```
  git add .
  git commit -m "Ghi chú về commit"
  git push origin <branch_name>
  ```
  *Lưu ý: khi này nội dung mới đang chỉ có trên brach riêng chưa hợp nhất với main*
  
- Tạo Pull Request để hợp nhất vào main hoặc nhờ leader (người quản lý nhánh main) hợp nhất hộ  
  *Với việc tạo Pull Request cần làm việc trực tiếp trên github*
- Chờ leader hoặc người quản lý nhánh main hợp nhất nhánh (các thành viên khác không cần để ý bước này)  
  - Với Pull Request tạo trên git thì sẽ cần kiểm tra và bấm "Merge Pull Request" khi sẵn sàng
  - Với yêu cầu merge thủ công  
    
    + Đảm bảo ở nhánh chính:
    ```  
      git fetch origin //Cập nhật các nhánh origin/... (remote-tracking) của GitHub (hiểu đơn giản là lấy thông tin)
      git checkout main
      git pull origin main
    ```
    + Merge từ nhánh thành viên  
    `git merge origin/branch_name`
    + (Nếu có xung đột) – sửa tay, sau đó:
    ```
      git add .
      git commit -m "..."
    ```
    + Push lên GitHub  
    `git push origin main`

- Sau khi merge xong thì các thành viên lấy nội dung mới qua các cách sau: 
  C1. Merge trực tiếp với main trên GitHub  
     *Đảm bảo đang làm việc trên nhánh cá nhân*
     ```
     git fetch origin //Cập nhật các nhánh origin/... (remote-tracking) của GitHub (hiểu đơn giản là lấy thông tin)
     git merge origin/main //Hợp nhất thẳng với phiên bản main trên GitHub
     ```
  C2. Thủ công hơn, cần cập nhật main local trước khi merge   
     ```
     git checkout main
     git pull origin main //Cập nhật main local chuẩn trước
     git checkout <branch_name>
     git merge main //Hợp nhất sau 
     ```
     *Có thể thay `git merge main` bằng `git rebase main` khi làm 1 mình hoặc 1 mình 1 nhánh để lịch sử commit gọn gàng*
  
  C3. Xoá nhánh cũ, tạo nhánh mới từ main (không khuyến khích)

  *Nếu xảy ra lỗi khi merge thì có thể ép pull về bằng cách reset nhánh*
  ```
  git fetch --all //Lấy toàn bộ thông tin mới trên GitHub
  git checkout <branch_name> //Chuyển sang nhánh cần reset
  git reset --hard origin/<branch_name> //Reset nhánh trên local về giống hệt nhánh trên GitHub
  ```

### Lưu ý:
- Phiên bản Unity được sử dụng ở đây là 6000.3.12f1
- Cần file .gitignore để khi git add. các file trong gitignore sẽ tự động được bỏ qua.
- Mỗi người làm việc với nhánh riêng để không gây xung đột và luôn kiểm tra trước khi push lên GitHub.
- File README.md là dùng cho dự án, file README_DEV.md dùng cho đội dev.
- Mọi người chú ý **tránh làm việc cùng 1 scene** đặc biệt scene chính, bất cứ hành động nhỏ nào cũng làm thay đổi scene và phải commit, khi merge sẽ dẫn đến xung đột với người đang làm việc trên scene đó.
- Nên tạo 1 scene riêng giống scene chính để làm và báo lại để người đang làm scene đó tự update thêm.
- Cần file .gitattributes để thiết lập các tùy chỉnh khi làm việc với github.
- Sử dụng công cụ UnityYAMLMerge của unity để merge những file YAML chuyên của Unity (thường có đuôi: .unity.prefab.asset.mat.controller). Để sử dụng:   
  + Thiết lập trong dự án: Edit → Project Settings → Editor → Asset Serialization = Force Text    
  + Mở Git Bash chạy các lệnh sau:   
    `git config --global merge.tool unityyamlmerge` 
    ```
    git config --global mergetool.unityyamlmerge.cmd \
    '".../6000.0.xf1/Editor/Data/Tools/UnityYAMLMerge.exe" merge -p "$BASE" "$REMOTE" "$LOCAL" "$MERGED"'
    ```  
    Đường dẫn đầy đủ và dùng / không dùng \   
    `git config --global mergetool.unityyamlmerge.trustExitCode false`    
- Sử dụng Git LFS để giảm dung lượng thư mục git mỗi lần update thêm asset    
  `git lfs version` Kiểm tra phiên bản, nếu chưa có thì tải về    
  `git lfs install` Cài đặt   
  `git config --get merge.tool` Kiểm tra lại cài đặt    
  `git lfs ls-files` Xem các file đang dùng   
- Một số lệnh khác có thể dùng đến:  
  `git merge --abort` Để nhánh quay lại trước khi merge   
  `git push origin <branch_name> --force` Ép gửi nội dung lên nhánh   
  `git reset --hard origin/<branch_name>` Ép reset nhánh hiện tại về giống nhánh branch_name trên GitHub    
  `git mergetool` Để dùng công cụ UnityYAMLMerge    