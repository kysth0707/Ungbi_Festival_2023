from fastapi import FastAPI
from starlette.middleware.cors import CORSMiddleware
import uvicorn

app = FastAPI()

origins = [
    "*"
]

app.add_middleware(
	CORSMiddleware,
	allow_origins=origins,
	allow_credentials=True,
	allow_methods=["*"],
	allow_headers=["*"]
)

nameFormat = [
	['고양이', '강아지', '거북이', '토끼', '뱀', '사자', '호랑이', '표범', '치타', '하이에나', '기린', '코끼리', '코뿔소', '하마', '악어', '펭귄', '부엉이', '올빼미', '곰', '돼지', '소', '닭', '독수리', '타조', '고릴라', '오랑우탄', '침팬지', '원숭이', '코알라', '캥거루', '고래', '상어', '칠면조', '직박구리', '쥐', '청설모', '메추라기', '앵무새', '삵', '스라소니', '판다', '오소리', '오리', '거위', '백조', '두루미', '고슴도치', '두더지', '우파루파', '맹꽁이', '너구리', '개구리', '두꺼비', '카멜레온', '이구아나', '노루', '제비', '까치', '고라니', '수달', '당나귀', '순록', '염소', '공작', '바다표범', '들소', '박쥐', '참새', '물개', '바다사자', '살모사', '구렁이', '얼룩말', '산양', '멧돼지', '카피바라', '도롱뇽', '북극곰', '퓨마', '', '미어캣', '코요테', '라마', '딱따구리', '기러기', '비둘기', '스컹크', '돌고래', '까마귀', '매', '낙타', '여우', '사슴', '늑대', '재규어', '알파카', '양', '다람쥐', '담비'],
	['위에', '아래에', '오른쪽에', '왼쪽에'],
	["감자깡", "계란과자", "고구마깡", "고래밥", "고소미", "구운감자", "국희 땅콩샌드", "그레이스", "꼬깔콘", "꼬북칩", "꽃게랑", "꿀꽈배기", "나", "나초", "노브랜드 라인", "눈을감자", "닭다리", "다이제", "닥터유 라인", "도리토스", "디샤", "롤리폴리", "롯데샌드", "롱스", "리얼 브라우니", "리츠", "마가렛트", "마이구미", "마이쮸", "마켓 오 라인", "맛동산", "맛밤", "무뚝뚝 감자칩", "미쯔", "바나나 킥", "버터링", "버터와플", "벌집핏자", "빅파이", "빈츠", "빠다코코낫", "빼빼로", "뽀또", "뽀빠이", "뿌셔뿌셔", "사또밥", "새우깡", "새콤달콤", "사브레", "새알", "스윙칩", "썬", "씨리얼", "아이비", "아이셔", "알새우칩", "야채타임", "야채크래커", "연양갱", "양파링", "에이스", "예감", "오감자", "오뜨", "오레오", "오예스", "오징어땅콩", "오징어집", "옥수수깡", "왕꿈틀이", "웨하스", "인디안밥", "인절미", "자가비", "자갈치", "쟈키쟈키", "제크", "죠리퐁", "짱구", "쫄병스낵", "찰떡파이", "찰떡쿠키", "참붕어빵", "참치크래커", "참크래커", "초코송이", "초코틴틴", "초코파이", "초코하임", "치토스", "칙촉", "칩포테토", "카라멜콘 땅콩", "카스타드", "칸쵸", "칼로리바란스", "콘치ㆍ콘초", "쿠크다스", "크라운 산도", "크림블", "팜 온 더 로드", "포스틱", "포키", "포테토칩", "허니버터칩", "홈런볼", "후레쉬베리", "후렌치 파이"]
]

print(f"단어 {len(nameFormat[0]) * len(nameFormat[1]) * len(nameFormat[2])}개 가능")
nameNumX, nameNumY, nameNumZ = 0, 0, 0

rankOfInfiniteStairs = []

def getName() -> str:
	global nameNumX, nameNumY, nameNumZ
	nameNumZ += 1
	if nameNumZ >= len(nameFormat[2]):
		nameNumZ = 0
		nameNumY += 1
	if nameNumY >= len(nameFormat[1]):
		nameNumY = 0
		nameNumX += 1
	return f"{nameFormat[0][nameNumX]}{nameFormat[1][nameNumY]}{nameFormat[2][nameNumZ]}"

@app.get('/')
def a():
	return True

@app.get('/add/infinitestairs/{score}')
def addDataToInfiniteStairs(score : int):
	global rankOfInfiniteStairs

	name = getName()
	rankOfInfiniteStairs.append([
		name, score
	])
	rankOfInfiniteStairs.sort()

	return {
		"name" : name
	}

@app.get('/get/infinitestairs')
def getScores():
	if len(rankOfInfiniteStairs) == 0:
		return ["없음", 0]
	return rankOfInfiniteStairs[::-1]


if __name__ == "__main__":
	uvicorn.run("app:app", host="0.0.0.0", port=999, reload=True)