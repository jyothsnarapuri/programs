#pragma once
#include "admin.h"
#include"store.h"
admin::admin()
using namespace std;
{
	//to determine whether new or existing user
	cout << "are you a new or existing user?" << endl;
	cout << "if new enter 1  else enter 0:";
	cin >> indicator;
	/*This will create new file if not created or open an existing file and append it*/
	ofstream reg("admininfo.txt", ios::app);
    //If new user
	if (indicator)
	{
		//password check to register as admin
		do
		{
			string adchec;
			cout << "Provide the password for access to register as admin:";
			cin >> adchec;
			if (adchec == "admin") {
				break;
			}
			else {
				cout << "\nyou can retry the registration process.Thank you\n";
				return;
			}
		} while (1);
		string s, p;
		cout << "enter the admin username:";
		cin >> s;
		cout << "the customer password with ther following criteria:\n1.length of the password should be greater than 8\n2.atleast 1 special ch";
		do
		{
			cout << "enter the adsmin password:";
			cin >> p;
			/*To make sure the admin password meets the criteria of:size of password greater than or equal to 8,atleast one uppercaseletter,
			atleast one uppercase letter,atleast one special character*/
			if (validatepassword(p) == 1) {
				break;
			}
			else {
				cout << "Give a password that meets the criterisa\n";

			}while (1);
			//assigning data members after validation
			name = s;
			password = p;
			admin_id = to_string(generaterandomnumber());
			//Writing the credentials into the adminfile
			reg << admin_id << ' ' << name << ' ' << passwordendl << endl;
			cout << "registration successful,your admin id has been generated:" << admin_id << "\n";
			reg.close();
		}
		//If the user has already registered
	else {
		string s, p;
		cout << "enter the admin the username:";
		cin >> s;
		name = s;
		cout << "enter the admin password:";
		cin >> p;
		//Authenticating credintials of existing user
		int exist = 0;
		string u, v, w;
		ifstream input("admininfo.txt");
		while (input >> u >> v >> w) {
			admin_id = u;
			if (v == name && w == password) {
				exist = 1;
				break;
			}
		}
		input.close();
		if (exist == 1) {
			cout << "Welcome admin" << admin_id << "_" << name << "\n";
			system("pause");
			bookshop bobj;
			obj.bookshopRecords();
			//bookshop obj;
		   //obj.bookshowerecords();
		}
		if (exist == 0) {
			cout << "your credentials are incorrect,kindly try again.\n";
		}
	}
	}
	//Methods to get private data members if needed besides being accessed by member functions
	string admin::getadminname()
	{
		return name;
	}
	string admin::getadminpassword() {
		return password;
	}
	//Methods to validate password criteria
	bool admin::detectUppercaseUse(string word)
	{
		int kw = 0;
		for (size_t i = 0; i < word.length(); i++) {
			if (isupper(word[i])) {
				kw = 1;
			}
		}
		if (kw == 1) {
			return true;
	}
		else {
			return false;
		}
	}
	int admin::validatepassword(string z) {
		int q = 0, e = 0, f = 0;
		if (z.length() >= 8) {
			q = 1;
		}
		string special = "[@_!#$^&*()<>?}{~:]";
		for (size_t ix = 0; ix < z.size(); ix++)
		{
			if (special.find(z[ix]) != string::npos)
			{
				e = 1;
				break;
			}
		}
		if (detectUppercaseUse(z)) {
			f = 1;
		}
		if ((q == 1 && e == 1) && f == 1) {
			return 1;
		}
		else {
			return 0;
		}
	}
	int admin::generaterandomnumber() {
		int max = 10000;//set the upper bound to generatethe random number
		srand(unsigned int)time(0));
		return rand() % max;
	 }
