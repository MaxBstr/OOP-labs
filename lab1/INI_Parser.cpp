//COPYRIGHT: MAX BASTRIKIN, ITMO UNIVERSITY, M3201
//INI FILES PARSER

#include <iostream>
#include <fstream>
#include <map>
#include <string>
#include <exception>


class Parser
{
private:
    std::map<std::string, std::map<std::string, std::string>> DATA;

    void tryToGetElement(std::string SECTION, std::string KEY) //check exist ? continue : warning
    {
        bool FindSection, FindKey;
        for (auto& section : DATA)
        {
            if(section.first == SECTION)
                FindSection = true;

            for (auto& key : section.second)
            {
                if(key.first == KEY)
                    FindKey = true;
            }
            if(FindKey && FindSection)
                break;
        }
        if(!FindSection)
        {
            std::cerr << "ERROR!\nCan`t get access! SECTION: " << SECTION << " doesn`t exist!\n";
            exit(1);
        }
        if(!FindKey)
        {
            std::cerr << "ERROR!\nCan`t get access! KEY: " << KEY << " doesn`t exist!\n";
            exit(1);
        }
    }

public:
    void Parse(std::string FILENAME)
    {
        std::ifstream file;
        file.open(FILENAME, std::ios_base::in);
        if (!file.is_open())
        {
            std::cerr << "ERROR! File can`t be open... Are you sure that you chose a correct file?\n";
            std::cerr << "You chose: " + FILENAME + '\n';
            exit(1);
        }

        std::string buff;
        std::string section;

        while(std::getline(file, buff))
        {
            bool isNewSection = false;
            int i = 0;

            if (buff.length() == 0)
                continue;

            if (buff[0] == '[')
                isNewSection = true;

            if(isNewSection)
            {
                section = "";
                while(buff[i] != ']')
                {
                    section += buff[i]; //got SECTION name
                    ++i;
                }
                section.erase(0, 1);
                continue;
            }
            //if str don`t start with letter
            if ((buff[0] >= 33 && buff[i] <= 64) || (buff[0] >= 94 && buff[0] <= 96) || (buff[0] >= 123 && buff[0] <= 127))//if broken file
            {
                if(buff[0] == ';' || buff[0] == '#' || buff[0] == ' ')
                    continue;
                else
                {
                    std::cerr << "ERROR! Broken file!\nVariables must start from a letter!\n";
                    exit(1);
                }
            }
            //if str starts with letter
            std::string key, value;
            while((i < buff.length()) && buff[i] != '=') //got KEY
            {
                if (buff[i] == ';' || buff[i] == '#')
                    break;

                if (buff[i] == ' ')
                {
                    ++i;
                    continue;
                }
                key += buff[i];
                ++i;
            }

            if (key.empty())
                continue;

            if(key.length() == buff.length())
            {
                DATA[section][key] = "";
                continue;
            }


            if (buff[i] != '=')
            {
                std::cerr << "ERROR! Broken file!\nComments must be after expression!!!\n";
                exit(1);
            }

            ++i; //ignore '=' symbol
            while((i < buff.length()) && buff[i] != ';' && buff[i] != '#')
            {
                if (buff[i] == ';' || buff[i] == '#')
                    break; //if comment - break cycle and get a new data

                if (buff[i] == ' ')
                {
                    ++i;
                    continue;
                }

                value += buff[i];
                ++i;
            }
            DATA[section][key] = value;
            key = "";
            value = "";
        }
        file.close();
    }

    bool TryGetInt(const std::string& SECTION, const std::string& KEY, int& result)
    {
        tryToGetElement(SECTION, KEY);
        //if no warning
        try
        {
            result = stoi(DATA[SECTION][KEY]);
            return true;
        }
        catch (const std::exception& WARNING){return false;}
    }

    bool TryGetDouble(const std::string& SECTION, const std::string& KEY, double& result)
    {
        tryToGetElement(SECTION, KEY);
        //if no warning
        try
        {
            result = stod(DATA[SECTION][KEY]);
            return true;
        }
        catch (const std::exception& WARNING){return false;}
    }

    template<typename T>
    bool TryGet(const std::string& SECTION, const std::string& KEY, T& result)
    {
        tryToGetElement(SECTION, KEY);
        int typeSize = sizeof(T);

        if (typeSize == 4) //int
        {
            try
            {
                result = stoi(DATA[SECTION][KEY]);
                return true;
            }
            catch (const std::exception& WARNING){return false;}
        }
        if (typeSize == 8) //double
        {
            try
            {
                result = stod(DATA[SECTION][KEY]);
                return true;
            }
            catch (const std::exception& WARNING){return false;}
        }
    }
};


int main()
{
    Parser myParser;
    myParser.Parse("TEST.ini"); //read DATA
    //successful tests
    double resultDouble;
    int resultInt;
    bool isCorrect;
    isCorrect = myParser.TryGet<double>("COMMON", "StatisterTimeMs", resultDouble);
    if(isCorrect)
    {
        std::cout << "Conversation was successful!\n";
        std::cout << resultDouble << std::endl;
    }

    isCorrect = myParser.TryGet<int>("ADC_DEV", "LogXML", resultInt);
    if(isCorrect)
    {
        std::cout << "Conversation was successful!\n";
        std::cout << resultInt << std::endl;
    }

    isCorrect = myParser.TryGetInt("COMMON", "OpenMPThreavasdsCount", resultInt);
    if(isCorrect)
    {
        std::cout << "Conversation was successful!\n";
        std::cout << resultInt << std::endl;
    }

    isCorrect = myParser.TryGetDouble("ADC_DEV", "BuffersLenSeconds", resultDouble);
    if(isCorrect)
    {
        std::cout << "Conversation was successful!\n";
        std::cout << resultDouble << std::endl;
    }

    //unsuccessful tests
    isCorrect = myParser.TryGet<int>("COMMON", "DiskCachePath", resultInt);
    if (!isCorrect)
        std::cerr << "Conversation wasn`t successful\n";

    isCorrect = myParser.TryGet<double>("ADC_DEV", "Drivers", resultDouble);
    if (!isCorrect)
        std::cerr << "Conversation wasn`t successful\n";

    isCorrect = myParser.TryGetInt("COMMON", "DiskCachePath", resultInt);
    if (!isCorrect)
        std::cerr << "Conversation wasn`t successful\n";

    isCorrect = myParser.TryGetDouble("ADC_DEV", "Drivers", resultDouble);
    if (!isCorrect)
        std::cerr << "Conversation wasn`t successful\n";

    return 0;
}
