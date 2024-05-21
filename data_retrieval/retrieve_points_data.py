
from selenium import webdriver
from webdriver_manager.chrome import ChromeDriverManager
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.chrome.service import Service
from selenium.common.exceptions import StaleElementReferenceException
import time

tabs = ["goals/", "attempts/", "distribution/", "attacking/", "defending/", "goalkeeping/"]
base_urls = ["https://www.uefa.com/european-qualifiers/statistics/teams/", "https://www.uefa.com/european-qualifiers/statistics/players/"]

service = Service(executable_path=ChromeDriverManager().install())

op = webdriver.ChromeOptions()
#op.add_argument('--headless')
op.add_argument('--window-size=1920x1080')
op.add_argument('--no-sandbox')
op.add_argument('--disable-dev-shm-usage')
op.add_argument('--disable-blink-features=AutomationControlled')
driver = webdriver.Chrome(service=service, options=op)
row_ids = {}
team_data = {}
player_data = {}

def extract_data(url, data):
    row_id = 0
    driver.get(url)
    if not "players" in url:  
        scroll_down()

    WebDriverWait(driver, 10).until(EC.presence_of_element_located((By.CSS_SELECTOR, ".ag-pinned-left-cols-container")))

    left_col_container = driver.find_element(By.CSS_SELECTOR, ".ag-pinned-left-cols-container")
    WebDriverWait(left_col_container, 10).until(EC.presence_of_all_elements_located((By.XPATH, ".//div[@role='row']")))
    rows = left_col_container.find_elements(By.XPATH, ".//div[@role='row']")
    print(len(rows))
    for row in rows:
        WebDriverWait(row, 10).until(EC.presence_of_element_located((By.CSS_SELECTOR, "div[col-id='identifier']")))
        identifier = row.find_element(By.CSS_SELECTOR, "div[col-id='identifier']")
        WebDriverWait(identifier, 10).until(EC.presence_of_element_located((By.CLASS_NAME, 'pk-w--100')))
        title = identifier.find_element(By.CLASS_NAME, "pk-w--100").get_attribute("title")
        if not title in data:
            data[title] = {}
        row_ids[row_id] = title
        row_id += 1
    row_id = 0
    mid_col_container = driver.find_element(By.CSS_SELECTOR, ".ag-center-cols-container")
    rows = mid_col_container.find_elements(By.XPATH, ".//div[@role='row']")
    for row in rows:
        categories = row.find_elements(By.CSS_SELECTOR, "div[role='gridcell']")
        for category in categories:
            value = category.find_element(By.CSS_SELECTOR, "span[role='presentation']").text
            category_name = category.get_attribute("col-id")
            data[row_ids[row_id]][category_name] = value
        row_id += 1
            
def scroll_down():
    """A method for scrolling the page."""

    stop_scrolling = 0

    while True:
        stop_scrolling += 20
        driver.execute_script("window.scrollBy(0, 1000);")

        time.sleep(3)


        if stop_scrolling > 100:
            break

if __name__ == "__main__":

    for tab in tabs:
        extract_data(base_urls[0] + tab, team_data)
    for tab in tabs:
        extract_data(base_urls[1] + tab, player_data)
    print(player_data)