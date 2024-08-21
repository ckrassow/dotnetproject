import os
import psycopg2
from dotenv import load_dotenv

load_dotenv()

MOST_GOALS = ["Spain"]
LEAST_CONCEDED = ["Serbia", "Slovenia", "Belgium"]
MOST_GOALS_GROUPS = ["Germany"]
MOST_YELLOWS = ["Turkey"]
MOST_PASSES = ["England"]
MOST_FOULS_AGAINST = ["Spain"]
MOST_FOULS_FOR = ["England"]
MOST_SHOTS_TAKEN = ["Spain"]
MOST_GOALS_BRACKET = ["Spain"]
WINNERS = ["Spain"]

CORRECT_PREDICTIONS_TEAMS = {
    0: ["Spain"],
    1: ["Serbia", "Slovenia", "Belgium"],
    2: ["Germany"],
    3: ["Turkey"],
    4: ["England"],
    5: ["Spain"],
    6: ["England"],
    7: ["Spain"],
    8: ["Spain"],
    9: ["Spain"]
}

CORRECT_PREDICTIONS_PLAYERS = {
    0: ["Georges Mikautadze", "Jamal Musiala", "Cody Gapko", "Dani Olmo", "Ivan Schranz", "Harry Kane"],
    1: ["Lamine Yamal"],
    2: ["Mike Maignan"],
    3: ["Rodri", "Dani Carvajal", "Silvan Widmer"],
    4: ["John Stones"],
    5: ["Álvaro Morata", "Virgil van Dijk"],
    6: ["Jude Bellingham"],
    7: ["Kylian Mbappé"],
    8: ["Virgil van Dijk"],
    9: ["Rodri"]
}

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

def get_player_name(player_string):
  if "(" in player_string:
    parts = player_string.split("(")
    player_name = parts[0].strip()
    return player_name
  else:
    return player_string.strip()  

def distribute_points(connection):

    try:

        reset_points(connection)

        cursor = connection.cursor()
        cursor.execute('SELECT * FROM "UserPlayerPredictions"')
        user_player_predictions = cursor.fetchall()

        for _, user_id, pred_id, _ in user_player_predictions:
            cursor.execute(
                'SELECT * FROM "PlayerPredictions" WHERE "Id" = %s', (pred_id,)
            )
            _, pred_type, player_id = cursor.fetchone()
            if player_id != None:
                cursor.execute(
                    'SELECT * FROM "Players" WHERE "Id" = %s', (player_id,)
                )
                player = cursor.fetchone()
                player_name = get_player_name(player[3])
                if player_name in CORRECT_PREDICTIONS_PLAYERS[pred_type]:
                    cursor.execute(
                        'SELECT * FROM "Users" WHERE "Id" = %s', (user_id,)
                    )
                    user_points = cursor.fetchone()[9]
                    new_user_points = user_points + 10
                    cursor.execute(
                        'UPDATE "Users" SET "Points" = %s WHERE "Id" = %s', (new_user_points, user_id,)
                    )
                    connection.commit()

        cursor.execute('SELECT * FROM "UserTeamPredictions"')
        user_team_predictions = cursor.fetchall()
        
        for _, user_id, pred_id, _ in user_team_predictions:
            cursor.execute(
                'SELECT * FROM "TeamPredictions" WHERE "Id" = %s', (pred_id,)
            )
            _, pred_type, team_id = cursor.fetchone()
            if team_id != None:
                cursor.execute(
                    'SELECT * FROM "NationalTeams" WHERE "Id" = %s', (team_id,)
                )
                team = cursor.fetchone()
                team_name = team[1]
                if team_name in CORRECT_PREDICTIONS_TEAMS[pred_type]:
                    cursor.execute(
                        'SELECT * FROM "Users" WHERE "Id" = %s', (user_id,)
                    )
                    user_points = cursor.fetchone()[9]
                    new_user_points = user_points + 10
                    cursor.execute(
                        'UPDATE "Users" SET "Points" = %s WHERE "Id" = %s', (new_user_points, user_id,)
                    )
                    connection.commit()


    except(Exception, psycopg2.Error) as error:

        print("Error while distributing points to users", error)

def reset_points(connection):

    try:
        cursor = connection.cursor()
        cursor.execute('SELECT * FROM "Users"')
        users = cursor.fetchall()

        for user in users:
            user_id = user[0]
            print(user[9])
            cursor.execute(
                'UPDATE "Users" SET "Points" = 0 WHERE "Id" = %s', (user_id,)
            )

            connection.commit()

            print(f"Points for user {user_id} reset to 0.")

    except(Exception, psycopg2.Error) as error:

        print("Error while resetting points", error)



def main():

    connection = connect_to_db()
    if connection is None:
        return
    distribute_points(connection)
    connection.close()

if __name__ == "__main__":
    main()
    
