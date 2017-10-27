#pragma once
#include "KReader.hpp"
#include "CURLWrapper.hpp"
#include "Types.hpp"
#include <deque>

class GameDataParser
{
public:
	GameDataParser()
	{
		m_kReader = new KReader;
		m_CURL = new CURLWrapper;
	}
	~GameDataParser()
	{
		delete m_kReader;
		delete m_CURL;
	}


	void readLoop()
	{
		json data;

		data["players"] = json::array();
		data["vehicles"] = json::array();
		data["items"] = json::array();
		data["zone"] = json::array();
		data["camera"] = json::array();

		readLocals();
		readPlayers(data);

		if (m_CURL->getReadyState() && !(data["items"].empty() && data["vehicles"].empty() && data["players"].empty()))
		{
			m_CURL->sendData(data.dump());
		}

	}

	int64_t getPUBase()
	{
		return m_kReader->getPUBase();
	}

	int64_t readPUBase()
	{
		return m_kReader->readPUBase();
	}


private:

	/*
	 * PRIVATE CLASS FUNCTIONS
	 */
	void readPlayers(json& w_data)
	{
		for (int i = 0; i < m_playerCount; i++)
		{
			// read the position of Player
			int64_t curActor = m_kReader->readType<int64_t>(m_AActorPtr + (i * 0x8), PROTO_NORMAL_READ);
			int32_t curActorID = m_kReader->readType<int32_t>(curActor + 0x0018, PROTO_NORMAL_READ);
			std::string actorGName = m_kReader->getGNameFromId(curActorID);

			// Here we check if the name is found from the wanted GNames list (PlayerMale etc...)
			if (std::find(playerIDs.begin(), playerIDs.end(), curActorID) != playerIDs.end())
			{

				int64_t rootCmpPtr = m_kReader->readType<int64_t>(curActor + 0x180, PROTO_NORMAL_READ);
				int64_t playerState = m_kReader->readType<int64_t>(curActor + 0x3C0, PROTO_NORMAL_READ);
				Vector3 actorLocation = m_kReader->readVec(rootCmpPtr + 0x1A0, PROTO_NORMAL_READ);

				int32_t actorTeam = m_kReader->readType<int32_t>(playerState + 0x0444, PROTO_NORMAL_READ);

				actorLocation.X += m_kReader->readType<int32_t>(m_PWorld + 0x918, PROTO_NORMAL_READ);
				actorLocation.Y += m_kReader->readType<int32_t>(m_PWorld + 0x91C, PROTO_NORMAL_READ);
				actorLocation.Z += m_kReader->readType<int32_t>(m_PWorld + 0x920, PROTO_NORMAL_READ);

				//TT
				float Health = m_kReader->readType<float>(curActor + 0x107C, PROTO_NORMAL_READ);
				Vector3 relativeLocation = m_kReader->readVec(rootCmpPtr + 0x01E0, PROTO_NORMAL_READ);

				Vector3 RelativeRotation = m_kReader->readVec(rootCmpPtr + 0x1EC, PROTO_NORMAL_READ);
				int32_t playerId = m_kReader->readType<int32_t>(playerState + 0x03C8, PROTO_NORMAL_READ);
				std::string playerName = "";
				unsigned char isInactive = m_kReader->readType<unsigned char>(playerState + 0x03CC, PROTO_NORMAL_READ);

				w_data["players"].emplace_back(json::object({ { "t", actorTeam },{ "x", actorLocation.X },{ "y", actorLocation.Y },{"z",actorLocation.Z},{ "rotator",RelativeRotation.Y},
					{ "rx", relativeLocation.X },{ "ry", relativeLocation.Y },{ "rz",relativeLocation.Z },
					//{"playerId",playerId }
					//,{ "playerName","" },
						{"health",Health },{"isInactive",isInactive } }));
			}

			if (actorGName == "DroppedItemGroup" || actorGName == "DroppedItemInteractionComponent")
			{
				int64_t rootCmpPtr = m_kReader->readType<int64_t>(curActor + 0x180, PROTO_NORMAL_READ);
				Vector3 actorLocation = m_kReader->readVec(rootCmpPtr + 0x1A0, PROTO_NORMAL_READ);
				int64_t DroppedItemArray = m_kReader->readType<int64_t>(curActor + 0x2D8, PROTO_NORMAL_READ);
				int32_t DroppedItemCount = m_kReader->readType<int32_t>(curActor + 0x2E0, PROTO_NORMAL_READ);

				for (int j = 0; j < DroppedItemCount; j++)
				{
					int64_t ADroppedItem = m_kReader->readType<int64_t>(DroppedItemArray + j * 0x10, PROTO_NORMAL_READ);
					Vector3 droppedLocation = m_kReader->readVec(ADroppedItem + 0x1E0, PROTO_NORMAL_READ);
					droppedLocation.X = droppedLocation.X + actorLocation.X + m_kReader->readType<int32_t>(m_PWorld + 0x918, PROTO_NORMAL_READ);
					droppedLocation.Y = droppedLocation.Y + actorLocation.Y + m_kReader->readType<int32_t>(m_PWorld + 0x91C, PROTO_NORMAL_READ);
					int64_t UItem = m_kReader->readType<int64_t>(ADroppedItem + 0x448, PROTO_NORMAL_READ);
					int32_t UItemID = m_kReader->readType<int32_t>(UItem + 0x18, PROTO_NORMAL_READ);
					std::string itemName = m_kReader->getGNameFromId(UItemID);

					// check if inside the map / array of wanted items
					for (std::map<std::string, std::string>::iterator it = dropGNameMap.begin(); it != dropGNameMap.end(); ++it)
					{
						if (itemName.substr(0, it->first.length()) == it->first)
						{
							int64_t rootCmpPtr = m_kReader->readType<int64_t>(curActor + 0x180, PROTO_NORMAL_READ);
							Vector3 actorLocation = m_kReader->readVec(rootCmpPtr + 0x1A0, PROTO_NORMAL_READ);

							actorLocation.X += m_kReader->readType<int32_t>(m_PWorld + 0x918, PROTO_NORMAL_READ);
							actorLocation.Y += m_kReader->readType<int32_t>(m_PWorld + 0x91C, PROTO_NORMAL_READ);
							actorLocation.Z += m_kReader->readType<int32_t>(m_PWorld + 0X920, PROTO_NORMAL_READ);
							Vector3 relativeLocation = m_kReader->readVec(rootCmpPtr + 0x01E0, PROTO_NORMAL_READ);

							w_data["items"].emplace_back(json::object({ { "n", it->second },{ "x", droppedLocation.X },{ "y", droppedLocation.Y },{"z",droppedLocation.Z },
							{ "rx", relativeLocation.X },{ "ry", relativeLocation.Y },{ "rz",relativeLocation.Z }

							}));
						}
					}
				}
			}

			else if (actorGName.substr(0, strlen("CarePackage")) == "CarePackage" || actorGName.substr(0, strlen("AircraftCarePackage")) == "AircraftCarePackage" || actorGName.substr(0, strlen("Carapackage_RedBox")) == "Carapackage_RedBox")
			{
				int64_t rootCmpPtr = m_kReader->readType<int64_t>(curActor + 0x180, PROTO_NORMAL_READ);
				int64_t playerState = m_kReader->readType<int64_t>(curActor + 0x3C0, PROTO_NORMAL_READ);
				Vector3 actorLocation = m_kReader->readVec(rootCmpPtr + 0x1A0, PROTO_NORMAL_READ);

				actorLocation.X += m_kReader->readType<int32_t>(m_PWorld + 0x918, PROTO_NORMAL_READ);
				actorLocation.Y += m_kReader->readType<int32_t>(m_PWorld + 0x91C, PROTO_NORMAL_READ);
				actorLocation.Z += m_kReader->readType<int32_t>(m_PWorld + 0x920, PROTO_NORMAL_READ);
				Vector3 relativeLocation = m_kReader->readVec(rootCmpPtr + 0x01E0, PROTO_NORMAL_READ);

				w_data["vehicles"].emplace_back(json::object({ { "v", "Drop" },{ "x", actorLocation.X },{ "y", actorLocation.Y },{ "z", actorLocation.Z } ,
				{ "rx", relativeLocation.X },{ "ry", relativeLocation.Y },{ "rz",relativeLocation.Z }
				}));

			}

			else if (std::find(allIDs.begin(), allIDs.end(), curActorID) == allIDs.end())
			{
				allIDs.push_back(curActorID);

				if (actorGName == "FAIL")
				{
					continue;
				}
				else
				{
					// iterate thru playerGnameVec
					for (std::vector<std::string>::iterator it = playerGNameVec.begin(); it != playerGNameVec.end(); ++it)
					{
						//check if the name is same, and add it to the playerIDs vector
						if (*it == actorGName.substr(0, (*it).length()))
						{
							playerIDs.push_back(curActorID);
							break;
						}
					}

					// iterate thru vehicleGNameVec
					for (std::vector<std::string>::iterator it = vehicleGNameVec.begin(); it != vehicleGNameVec.end(); ++it)
					{
						//check if the name is same, and add it to the vehicleIDs vector
						if (*it == actorGName.substr(0, (*it).length()))
						{
							vehicleIDs.push_back(curActorID);
							break;
						}
					}
				}
			}
			else if (std::find(vehicleIDs.begin(), vehicleIDs.end(), curActorID) != vehicleIDs.end())
			{
				// tästä alaspäin voi tehdä if-lohkoissa
				int64_t rootCmpPtr = m_kReader->readType<int64_t>(curActor + 0x180, PROTO_NORMAL_READ);
				Vector3 actorLocation = m_kReader->readVec(rootCmpPtr + 0x1A0, PROTO_NORMAL_READ);

				actorLocation.X += m_kReader->readType<int32_t>(m_PWorld + 0x918, PROTO_NORMAL_READ);
				actorLocation.Y += m_kReader->readType<int32_t>(m_PWorld + 0x91C, PROTO_NORMAL_READ);

				std::string carName = m_kReader->getGNameFromId(curActorID);

				w_data["vehicles"].emplace_back(json::object({ { "v", carName.substr(0, 3) },{ "x", actorLocation.X },{ "y", actorLocation.Y } }));

			}
		}
		//TT
		//w_data["zone"].emplace_back(json::object({ { "n","safe" },{ "X" , safe_zone.X },{ "Y",safe_zone.Y },{ "Z",safe_zone.Z },{ "R",safe_Radius },{"NumAlivePlayers",NumAlivePlayers } }));
		//w_data["zone"].emplace_back(json::object({ { "n","poison" },{ "X" , poison_zone.X },{ "Y",poison_zone.Y },{ "Z",poison_zone.Z },{ "R",poison_Radius } }));
		//w_data["zone"].emplace_back(json::object({ { "n","red"},{"X" , red_zone.X },{"Y",red_zone.Y},{ "Z",red_zone.Z },{"R",red_Radius } }));
		Vector3 povRotation = m_kReader->readVec(m_localPlayerCamerManager + 0x42C, PROTO_NORMAL_READ);
		w_data["camera"].emplace_back(json::object({ { "n","Rotation"},{"X" , povRotation.X },{"Y",povRotation.Y},{ "Z",povRotation.Z } }));
		Vector3 povLocation = m_kReader->readVec(m_localPlayerCamerManager + 0x420, PROTO_NORMAL_READ);
		w_data["camera"].emplace_back(json::object({ { "n","Location" },{ "X" , povLocation.X },{ "Y",povLocation.Y },{ "Z",povLocation.Z }}));
		float Fov = m_kReader->readType<float>(m_localPlayerCamerManager + 0x438, PROTO_NORMAL_READ);
		w_data["camera"].emplace_back(json::object({ { "n","Fov" },{ "X",Fov },{ "Y",0 },{ "Z",0 } }));


		//get zone info
		Vector3 blue = m_kReader->readVec(ATslGameState + 0x440, PROTO_NORMAL_READ);
		float blueR = m_kReader->readType<float>(ATslGameState + 0x44C, PROTO_NORMAL_READ);
		w_data["zone"].emplace_back(json::object({ { "r", blueR },{ "x", blue.X },{ "y", blue.Y } }));

		Vector3 white = m_kReader->readVec(ATslGameState + 0x450, PROTO_NORMAL_READ);
		float whiteR = m_kReader->readType<float>(ATslGameState + 0x45C, PROTO_NORMAL_READ);
		w_data["zone"].emplace_back(json::object({ { "r", whiteR },{ "x", white.X },{ "y", white.Y } }));

	}

	void readLocals()
	{
		m_UWorld = m_kReader->readType<int64_t>(m_kReader->getPUBase() + 0x37E5988, PROTO_NORMAL_READ);
		m_gameInstance = m_kReader->readType<int64_t>(m_UWorld + 0x140, PROTO_NORMAL_READ);
		m_ULocalPlayer = m_kReader->readType<int64_t>(m_gameInstance + 0x38, PROTO_NORMAL_READ);
		m_localPlayer = m_kReader->readType<int64_t>(m_ULocalPlayer+0x0, PROTO_NORMAL_READ);
		m_viewportclient = m_kReader->readType<int64_t>(m_localPlayer + 0x58, PROTO_NORMAL_READ);
		m_localPawn = m_kReader->readType<int64_t>(m_localPlayer + 0x3A8, PROTO_NORMAL_READ);
		m_localPlayerState = m_kReader->readType<int64_t>(m_localPawn + 0x03C0, PROTO_NORMAL_READ);
		m_PWorld = m_kReader->readType<int64_t>(m_viewportclient + 0x80, PROTO_NORMAL_READ);
		m_ULevel = m_kReader->readType<int64_t>(m_PWorld + 0x30, PROTO_NORMAL_READ);
		m_playerCount = m_kReader->readType<int32_t>(m_ULevel + 0xA8, PROTO_NORMAL_READ);

		//TT
		m_localPlayerControl = m_kReader->readType<int64_t>(m_localPlayer + 0x30, PROTO_NORMAL_READ);
		m_localPlayerCamerManager = m_kReader->readType<int64_t>(m_localPlayerControl + 0x438, PROTO_NORMAL_READ);
		m_localPlayerCamerCache = m_kReader->readType<int64_t>(m_localPlayerCamerManager + 0x410, PROTO_NORMAL_READ);
		Pov = m_kReader->readType<int64_t>(m_localPlayerCamerManager + 0x10 , PROTO_NORMAL_READ);


		ATslGameState = m_kReader->readType<int64_t>(m_PWorld + 0x00F8, PROTO_NORMAL_READ);
		//safe_zone.X; m_kReader->readType<float>(ATslGameState + 0x0440, PROTO_NORMAL_READ);
		//safe_zone.Y; m_kReader->readType<float>(ATslGameState + 0x0444, PROTO_NORMAL_READ);
		//safe_zone.Z; m_kReader->readType<float>(ATslGameState + 0x0448, PROTO_NORMAL_READ);
		//safe_Radius = m_kReader->readType<float>(ATslGameState + 0x044C, PROTO_NORMAL_READ);
		//NumAlivePlayers =  m_kReader->readType<float>(ATslGameState + 0x0430, PROTO_NORMAL_READ);
		//poison_zone; m_kReader->readType<float>(ATslGameState + 0x0450, PROTO_NORMAL_READ);
		//poison_Radius = m_kReader->readType<float>(ATslGameState + 0x045C, PROTO_NORMAL_READ);
		//red_zone; m_kReader->readType<float>(ATslGameState + 0x0460, PROTO_NORMAL_READ);
		//red_Radius = m_kReader->readType<float>(ATslGameState + 0x046C, PROTO_NORMAL_READ);

		m_localPlayerPosition = m_kReader->readVec(m_localPlayer + 0x70, PROTO_NORMAL_READ);
		m_localPlayerBasePointer = m_kReader->readType<int64_t>(m_localPlayer, PROTO_NORMAL_READ);

		m_localTeam = m_kReader->readType<int32_t>(m_localPlayerState + 0x0444, PROTO_NORMAL_READ);

		m_AActorPtr = m_kReader->readType<int64_t>(m_ULevel + 0xA0, PROTO_NORMAL_READ);
	}



	/*
	 * CLASS VARIABLES
	 */
	KReader* m_kReader;
	CURLWrapper* m_CURL;


	/*
	 * Local variables
	 * These are updated once every read loop.
	 */
	int64_t m_UWorld;
	int64_t m_gameInstance;
	int64_t m_ULocalPlayer;
	int64_t m_localPlayer;
	int64_t m_viewportclient;
	int64_t m_localPawn;
	int64_t m_localPlayerState;
	int64_t m_PWorld;
	int64_t m_ULevel;
	int32_t m_playerCount;

	//TT
	int64_t m_localPlayerControl;
	int64_t m_localPlayerCamerManager;
	int64_t m_localPlayerCamerCache;
	int64_t Pov;

		int64_t ATslGameState;
	Vector3 safe_zone;
	float safe_Radius;
	Vector3 poison_zone;
	float poison_Radius;
	Vector3 red_zone;
	float red_Radius;
	float NumAlivePlayers;


	Vector3 m_localPlayerPosition;
	int64_t m_localPlayerBasePointer;
	int32_t m_localTeam;
	int64_t m_AActorPtr;

	/*
	 * Global IDs that are found from the game
	 * These containers are used to help the
	 * maintaining of systematic ID handling and
	 * storing.
	 */
	std::deque<int32_t> allIDs;

	std::vector<int32_t> playerIDs;
	std::vector<int32_t> vehicleIDs;
};
