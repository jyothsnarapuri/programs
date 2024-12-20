#pragma once
#include<iostream>
#include<string>
#include<fstream>
#include<cctype>
#include<cstdlib>
class admin {
private:
	int indicator;
	std::string admin_id;
	std::string name;
	std::string password;
public:
	admin();
	std::string getadminname();
	std::string getadminpassword();
	int validatepassword(std::string);
	bool detectUppercaseUse(std::string);
	int generaterandomnumber();
};
