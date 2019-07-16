Feature: ShopLoginTests Chrome
	Verification of the shop authentication

@UiTest
@ChromeLocal
@issue:111
@blocker
Scenario Outline: Shop login tests Chrome
	Given I am navigated to Shop application main page
	When I click Sign in link
	And I login using <email> and <password>
	Then I should see next <message> message
	Examples: 
	| email        | password | message                    |
	| uk@gmail.com | pass     | Invalid password.          |
	| uk@gmail.co  | 123456   | Authentication failed.     |
	| test         | pass     | Invalid email address.     |
	|              | 123456   | An email address required. |
	| uk@gmail.com |          | Password is required.      |

@minor
Scenario: Validating that Cart is empty by default
	Given I am navigated to Shop application main page
	Then I see that cart has (empty) default value