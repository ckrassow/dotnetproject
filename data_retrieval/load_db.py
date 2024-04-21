import os
import pandas as pd
import psycopg2
from psycopg2 import sql
from dotenv import load_dotenv

load_dotenv()

def connect_to_db():
    try:
        connection = psycopg2.connect(
            user=os.getenv('DB_USER'),
            password=os.getenv('DB_PASSWORD'),
            host=os.getenv('DB_HOST'),
            port=os.getenv('DB_PORT'),
            database=os.getenv('DB_NAME')
        )
        return connection
    
    except (Exception, psycopg2.Error) as error:
        
        print("Error while connecting to PostgreSQL", error)
        return None

def get_national_team_id(cursor, team_name):
    
    cursor.execute('SELECT "Id" FROM "NationalTeams" WHERE "Name" = %s', (team_name, ))
    result = cursor.fetchone()

    if result:

        return result[0]
    else:

        return None

def get_image_path(folder, name):
    image_name = name.replace(" ", "_")
    for file in os.listdir(folder):
        if file.startswith(image_name):
            return os.path.join(folder, file)
    return "default.jpg"

def insert_data(connection):

    try:
        cursor = connection.cursor()

        team_df = pd.read_csv("data/euro_team_data.csv")
        for _, row in team_df.iterrows():
            fifa_ranking = row['FIFA_rankings'] if not pd.isna(row['FIFA_rankings']) else 0
            image_path = get_image_path("data/team_images", row["Team"])
            cursor.execute(sql.SQL("""
                INSERT INTO "NationalTeams" ("Name", "PlayoffAppearences", "FifaRanking", "Group", "ImagePath")
                VALUES (%s, %s, %s, %s, %s)
            """), (row["Team"], row["Playoff_appearences"], fifa_ranking, row["Group"], image_path))
        
        player_df = pd.read_csv("data/euro_player_data.csv")
        for _, row in player_df.iterrows():
            national_team_id = get_national_team_id(cursor, row["Team"])
            no = int(row['No.']) if row['No.'] != -1 else 0
            image_path = get_image_path("data/player_images", row["Player"])
            cursor.execute(sql.SQL("""
                INSERT INTO "Players" ("No", "Pos", "Name", "Age", "Caps", "Goals", "Club", "NationalTeamId", "ImagePath")
                VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s)
            """), (no, row["Pos."], row["Player"], row["Age"], row["Caps"], row["Goals"], row["Club"], national_team_id, image_path))
        
        connection.commit()
        print("Data inserted successfully")
    
    except (Exception, psycopg2.Error) as error:

        print("Error while inserting data into tables", error)
        print(row)

def main():

    connection = connect_to_db()
    if connection is None:
        return

    insert_data(connection)
    connection.close()

if __name__ == "__main__":
    main()