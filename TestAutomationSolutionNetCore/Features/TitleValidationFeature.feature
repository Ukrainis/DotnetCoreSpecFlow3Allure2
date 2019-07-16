Feature: TitleValidationFeature

@UiTest
@Chrome
@tms:333
@trivial
Scenario: Verifying title of the Home page
	Given I am navigated to Shop application main page
	Then I see that page title equals to "My Store"
