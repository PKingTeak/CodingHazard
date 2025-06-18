# 🧟‍♂️ FPS 좀비 서바이벌 프로젝트

**Unity 기반 1인칭 좀비 서바이벌 게임**  
플레이어는 좀비로 가득 찬 세계에서 각종 아이템을 수집하고, 좀비와 전투를 벌이며, 다양한 상호작용과 퀘스트를 통해 목표 지점까지 무사히 도달해야 하는 역동적인 1인칭 액션 FPS 게임입니다.

---

## 📽️ 주요 기능

### 🏠 1. 로비 시스템
> 게임 시작 전, 게임의 정체성을 나타낼 수 있는 Lobby System입니다.
> 화면에서 Stage 선택 또는 게임 종료를 선택할 수 있습니다.
![lobby.gif](https://github.com/PKingTeak/CodingHazard/blob/main/Assets/gif/Lobby.gif)

---

### 📦 2. 로딩 화면
> 게임 Scene 전환 시 자연스러운 연결을 위한 연출입니다.
> 현재 로딩 상태를 표시하여 플레이어의 시각적 효과를 증가시켰습니다.
![loading.gif](https://github.com/PKingTeak/CodingHazard/blob/main/Assets/gif/Roading.gif)

---

### 🎬 3. 인트로 연출
> 게임의 세계관 전달과 플레이어의 몰입도를 높일 수 있는 게임의 첫 장면입니다.
![intro.gif](https://github.com/PKingTeak/CodingHazard/blob/main/Assets/gif/Intro.gif)

---

### 🚶‍♂️ 4. 자유 이동 & 카메라 이벤트
> FSM을 통한 플레이어의 이동과 플레이어의 상태에 따른 다른 카메라 연출을 보여 줍니다.
> 1인칭 시점인 만큼 흔들림, 줌인/줌아웃 등의 카메라 이벤트를 통해 배경 연출, 심리적 긴장감을 전달해 줍니다.
![walk_camera_event.gif](https://github.com/PKingTeak/CodingHazard/blob/main/Assets/gif/PlayerMovement.gif)

---

### 👁️ 5. 좀비 시야 감지 시스템
> 좀비는 특정 거리 내의 플레이어를 감지할 수 있습니다.
> 좀비에게 발각되면 플레이어 UI로 확인할 수 있습니다.
![zombie_vision.gif](https://github.com/PKingTeak/CodingHazard/blob/main/Assets/gif/Detect.gif)

---

### 🔫 6. 총기 사격 & 좀비 타격
> 원거리 무기를 사용해 좀비를 공격할 수 있습니다.
> 조준 상태와 비조준 상태로 공격이 가능하며, 각각의 상태에 따라 정확도가 달라집니다.
#### 🎯 조준 사격
![aim_fire_zombie.gif](https://github.com/PKingTeak/CodingHazard/blob/main/Assets/gif/Aim.gif)

#### ❌ 비조준 사격
![hip_fire_zombie.gif](https://github.com/PKingTeak/CodingHazard/blob/main/Assets/gif/not%20Aim.gif)

---

### 🪓 7. 근접 무기 타격
> 탄약 부족 상황이나 총소리가 나지 않는 조용한 전투 상황에 사용할 수 있습니다.
> 애니메이션과 연동된 피격 반응을 통해 타격감이 강조됩니다.
![melee_attack.gif](https://github.com/PKingTeak/CodingHazard/blob/main/Assets/gif/MeleeWeapon.gif)

---

### 💥 8. 플레이어 피격 & 상태 변화
> 좀비에게 공격 당하면 플레이어 체력이 감소합니다.
> 플레이어 피격 상황에는 UI와 사운드를 통해 직관적으로 전달되며,
> 체력이 바닥났을 경우에는 Game Over가 됩니다.
![player_hit.gif](https://github.com/PKingTeak/CodingHazard/blob/main/Assets/gif/HitPlayer.gif)

---

### 🤝 9. 상호작용 시스템
> 게임을 클리어 하기 위한 퀘스트 아이템 습득이나 생존에 필요한 아이템을 습득할 수 있는 상호작용 시스템입니다.
> Raycast 기반으로 직접 상호작용을 실행하며, 플레이어와 일정 거리 내에 있는 아이템은 Outline을 표시합니다.
![interaction.gif](https://github.com/PKingTeak/CodingHazard/blob/main/Assets/gif/Interact.gif)

---

### 📜 10. 퀘스트 시스템
> 전반적인 게임 진행을 위한 퀘스트가 부여되어 게임의 목적성을 갖추었습니다.
> 조건을 달성하여 게임을 클리어할 수 있습니다.
![quest_system.gif](https://github.com/PKingTeak/CodingHazard/blob/main/Assets/gif/Quest.gif)

---

### ⚙️ 11. 환경 설정
> 게임 중 ESC 키를 통하여 환경 설정으로 진입할 수 있습니다.
> 마우스 감도나 Fov 등의 디스플레이 설정과 각종 사운드 조절이 가능한 오디오 설정이 있습니다.
![settings.gif](https://github.com/PKingTeak/CodingHazard/blob/main/Assets/gif/Setting.gif)

---

### 📷 12. 피사계 심도 연출
> 플레이어의 몰입감 있는 연출을 위한 시각 효과입니다.
> 플레이어가 가까운 오브젝트에 접근할 경우, 해당 오브젝트에 포커스를 주고 주변을 블러 처리합니다.
![depth_of_field.gif](https://github.com/PKingTeak/CodingHazard/blob/main/Assets/gif/DOF.gif)

---

## 🔧 기술 스택

- Unity (C#)
- Input System
- FSM (Finite State Machine)
- ScriptableObject 기반 데이터 관리
- CharacterController + ForceReceiver 이동/점프 구조
- Animator 기반 연출 제어

---

## 📌 향후 계획

- 세이브/로드 시스템 구현  
- 장비 시스템 UI/UX 정비  
- 다양한 스테이지 구현
- ...

---

## 🗂️ 프로젝트 구조
> ???
